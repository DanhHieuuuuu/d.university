FROM python:3.10-slim

WORKDIR /app

# Copy riêng requirements để tận dụng cache Docker
COPY requirements.txt .

# Nâng pip + cài thư viện
RUN pip install --upgrade pip \
    && pip install -r requirements.txt

# Copy source code
COPY . .

# Run FastAPI
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "10000"]
