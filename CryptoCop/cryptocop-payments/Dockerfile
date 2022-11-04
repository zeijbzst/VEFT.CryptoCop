FROM python:3.9.6 
LABEL maintainer="Logi Páll Sævarsson"

WORKDIR /app

COPY requirements.txt requirements.txt
RUN pip install -r requirements.txt

COPY . .

ENV HOST rabbitmq

CMD ["python", "./payment_service.py"]