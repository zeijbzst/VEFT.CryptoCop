import pika
import requests
import json
import os

host = os.getenv('HOST', 'localhost')
connection = pika.BlockingConnection(pika.ConnectionParameters(host))
channel = connection.channel()
exchange_name = 'crypto_exchange'
create_order_routing_key = 'create-order'
email_queue_name = 'email-queue'

# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
# Declare the queue, if it doesn't exist
channel.queue_declare(queue=email_queue_name, durable=True)
# Bind the queue to a specific exchange with a routing key
channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)

def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandbox8488de301fcf43419acbd0243c58224f.mailgun.org/messages",
        auth=("api", "5a4d3b38c3347c2d8b615b6be6ad6f33-d117dd33-d9a8f8fb"),
        data={"from": "Mailgun Sandbox <postmaster@sandbox8488de301fcf43419acbd0243c58224f.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})


def send_order_email(ch, method, properties, data):
    msg = json.loads(data)
    items = msg['orderItems']
    items_html = ''.join([ f'<tr><td>{item["id"]}</td><td>{item["productIdentifier"]}</td><td>{item["quantity"]}</td><td>{item["unitPrice"]}</td><td>{item["totalPrice"]}</td></tr>' for item in items])
    order_info_html = f'<p>Thank you for ordering {msg["fullName"]}!</p><h2>Address</h2><p>{msg["streetName"]} {msg["houseNumber"]}</p><p>{msg["city"]}, {msg["zipCode"]}</p><p>{msg["country"]}</p><p>Date of order: {msg["orderDate"]}</p><p>Total price: {msg["totalPrice"]}</p><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Id number</th><th>Product Identifier</th><th>Quantity</th><th>Unit price</th><th>Row price</th></tr></thead><tbody>{items_html}</tbody></table>'

    send_simple_message(msg['email'], 'Successful order!', order_info_html)

channel.basic_consume(on_message_callback=send_order_email,
                      queue=email_queue_name,
                      auto_ack=True)

print("Wating for new orders :)")
channel.start_consuming()
connection.close()
