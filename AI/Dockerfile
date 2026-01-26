FROM python:3.10-slim

# ===== 1. Cài system dependencies =====
RUN apt-get update && apt-get install -y \
    curl \
    gnupg \
    unixodbc \
    unixodbc-dev \
    gcc \
    g++ \
    && rm -rf /var/lib/apt/lists/*

# ===== 2. Cài Microsoft ODBC Driver 18 (Debian 12 chuẩn mới) =====
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc \
    | gpg --dearmor -o /usr/share/keyrings/microsoft.gpg && \
    echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] \
    https://packages.microsoft.com/debian/12/prod bookworm main" \
    > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql18 && \
    rm -rf /var/lib/apt/lists/*

# ===== 3. App setup =====
WORKDIR /app

COPY requirements.txt .

RUN pip install --upgrade pip \
    && pip install --no-cache-dir -r requirements.txt

COPY . .

# ===== 4. Run FastAPI (Render bắt buộc dùng $PORT) =====
CMD ["sh", "-c", "uvicorn main:app --host 0.0.0.0 --port $PORT"]
