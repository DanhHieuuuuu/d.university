# D.University Chatbot API

API Chatbot tu van sinh vien su dung RAG (Retrieval Augmented Generation) voi Groq LLM.

## Cau truc thu muc

```
Chatbot/
├── app/
│   ├── __init__.py
│   ├── config.py           # Cau hinh
│   ├── main.py             # FastAPI server
│   ├── models/
│   │   ├── __init__.py
│   │   └── schemas.py      # Pydantic schemas
│   └── services/
│       ├── __init__.py
│       ├── chunking.py     # Chia nho du lieu JSON
│       ├── embedding.py    # Tao embedding vector
│       ├── vector_store.py # Luu tru va tim kiem vector
│       ├── groq_client.py  # Goi Groq API
│       └── rag.py          # RAG pipeline
├── data/
│   ├── students_rag_data.json  # Du lieu sinh vien
│   └── vector_store/           # Luu tru index (tu dong tao)
├── .env                    # Cau hinh (can tao)
├── .env.example            # Mau cau hinh
├── requirements.txt        # Thu vien can thiet
└── README.md
```

## Cai dat

### 1. Tao moi truong ao (khong bat buoc nhung khuyen nghi)

```bash
python -m venv venv
.\venv\Scripts\activate  # Windows
```

### 2. Cai dat thu vien

```bash
pip install -r requirements.txt
```

### 3. Cau hinh

Tao file `.env` tu `.env.example`:

```bash
copy .env.example .env
```

Sua file `.env` va them Groq API key:

```
GROQ_API_KEY=your_groq_api_key_here
GROQ_MODEL=llama-3.3-70b-versatile
EMBEDDING_MODEL=sentence-transformers/paraphrase-multilingual-MiniLM-L12-v2
```

> Lay Groq API key tai: https://console.groq.com/keys

### 4. Chay server

```bash
uvicorn app.main:app --reload --host 0.0.0.0 --port 8000
```

Hoac:

```bash
python -m app.main
```

## API Endpoints

### 1. Health Check

```
GET /
GET /health
```

### 2. Chat voi Chatbot

```
POST /api/chat
```

Body:
```json
{
  "message": "Diem hoc ky 2 cua toi la bao nhieu?",
  "conversation_history": []
}
```

Response:
```json
{
  "response": "Diem trung binh hoc ky 2 cua ban la 8.28...",
  "context_used": ["..."]
}
```

### 3. Lay Dinh huong Hoc tap

```
GET /api/orientation
```

Response:
```json
{
  "orientation": "Dua tren ket qua hoc tap cua ban..."
}
```

### 4. Lay Tom tat Thong tin Sinh vien

```
GET /api/student/summary
```

### 5. Rebuild Index

Goi khi du lieu JSON thay doi:

```
POST /api/rebuild-index
```

## Ky thuat su dung

### Chunking
- **Semantic Chunking**: Chia du lieu JSON theo ngu nghia (thong tin sinh vien, diem, chuong trinh khung...)
- Moi chunk giu nguyen tinh toan ven cua thong tin

### Embedding
- **Model**: `paraphrase-multilingual-MiniLM-L12-v2`
- Ho tro tot tieng Viet va da ngon ngu
- Dimension: 384

### Vector Store
- **FAISS**: Thu vien tim kiem vector nhanh cua Facebook
- Luu index ra file de khong phai rebuild moi lan

### LLM
- **Groq API**: Goi truc tiep API khong qua LangChain
- Model mac dinh: `llama-3.3-70b-versatile`

## Tuy chinh

### Thay doi model LLM

Sua file `.env`:
```
GROQ_MODEL=mixtral-8x7b-32768
```

### Thay doi model embedding

Sua file `.env`:
```
EMBEDDING_MODEL=sentence-transformers/paraphrase-multilingual-mpnet-base-v2
```

Sau do rebuild index:
```bash
curl -X POST http://localhost:8000/api/rebuild-index
```

### Them du lieu sinh vien moi

1. Sua file `data/students_rag_data.json`
2. Goi API rebuild index

## Luu y

- Server tu dong tai model embedding lan dau khoi dong (mat vai phut)
- Index duoc luu tai `data/vector_store/` de su dung lai
- Lich su hoi thoai duoc gui kem de chatbot hieu ngu canh
