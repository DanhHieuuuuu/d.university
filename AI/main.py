from fastapi import FastAPI
from models.delegation_schema import SearchRequest
from services.ai_service import extract_conditions
from services.search_service import search_delegation_incoming
from fastapi.middleware.cors import CORSMiddleware
app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=[
        "http://localhost:3077",
        "http://127.0.0.1:3077",
        "https://d-university-core-jk86.onrender.com",
        "https://d-university-core.onrender.com",
        "https://d-university-9zz7.onrender.com",
        "https://d-university.onrender.com",
        "https://d-university-3333.vercel.app",
        "https://d-university-5.vercel.app",
    ],
    allow_credentials=True,
    allow_methods=["*"],  
    allow_headers=["*"],  
)

@app.post("/api/ai-search")
async def ai_search(req: SearchRequest):
    conditions = extract_conditions(req.query)
    data = search_delegation_incoming(conditions)

    return {
        "conditions": conditions,
        "data": data
    }

