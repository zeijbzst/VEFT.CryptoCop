import pika
import json
import os
from luhn import *

host = os.getenv('HOST', 'localhost')
connection = pika.BlockingConnection(pika.ConnectionParameters(host))
channel = connection.channel()
exchange_name = 'crypto_exchange'
create_order_routing_key = 'create-order'
email_queue_name = 'payment-queue'

# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
# Declare the queue, if it doesn't exist
channel.queue_declare(queue=email_queue_name, durable=True)
# Bind the queue to a specific exchange with a routing key
channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)

def validate_credit_cart(ch, method, properties, data):
    msg = json.loads(data)
    if(verify(msg['creditCard'])):
        print("Credit card is valid!")
    else:
        print("Invalid credit card!")

channel.basic_consume(on_message_callback=validate_credit_cart,
                      queue=email_queue_name,
                      auto_ack=True)

print("Wating for new orders :)")
channel.start_consuming()
connection.close()
