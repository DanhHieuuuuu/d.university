import os
from dotenv import load_dotenv

load_dotenv()


class Config:
    """Cấu hình ứng dụng từ biến môi trường."""
    
    # Groq API
    GROQ_API_KEY: str = os.getenv("GROQ_API_KEY", "")
    GROQ_API_URL: str = "https://api.groq.com/openai/v1/chat/completions"
    GROQ_MODEL: str = os.getenv("GROQ_MODEL", "llama-3.3-70b-versatile")
    
    # Embedding
    EMBEDDING_MODEL: str = os.getenv(
        "EMBEDDING_MODEL", 
        "sentence-transformers/paraphrase-multilingual-MiniLM-L12-v2"
    )
    
    # RAG
    TOP_K_RESULTS: int = 5
    
    # ChromaDB
    CHROMA_PERSIST_DIR: str = os.path.join(os.path.dirname(__file__), "..", "data", "chroma_db")
    CHROMA_COLLECTION_NAME: str = "student_data"


config = Config()
