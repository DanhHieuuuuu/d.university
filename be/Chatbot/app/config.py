import os
from dotenv import load_dotenv

load_dotenv()

class Config:
    # Groq API
    GROQ_API_KEY: str = os.getenv("GROQ_API_KEY", "")
    GROQ_API_URL: str = "https://api.groq.com/openai/v1/chat/completions"
    GROQ_MODEL: str = os.getenv("GROQ_MODEL", "llama-3.3-70b-versatile")
    
    # Embedding
    EMBEDDING_MODEL: str = os.getenv(
        "EMBEDDING_MODEL", 
        "sentence-transformers/paraphrase-multilingual-MiniLM-L12-v2"
    )
    
    # Chunking
    CHUNK_SIZE: int = 500
    CHUNK_OVERLAP: int = 50
    
    # RAG
    TOP_K_RESULTS: int = 5
    
    # Paths
    DATA_PATH: str = os.path.join(os.path.dirname(__file__), "..", "data", "students_rag_data.json")
    VECTOR_STORE_PATH: str = os.path.join(os.path.dirname(__file__), "..", "data", "vector_store")
    
    # ChromaDB
    CHROMA_PERSIST_DIR: str = os.path.join(os.path.dirname(__file__), "..", "data", "chroma_db")
    CHROMA_COLLECTION_NAME: str = "student_data"

config = Config()
