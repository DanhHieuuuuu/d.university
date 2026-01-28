import os
from dotenv import load_dotenv
from dataclasses import dataclass
from typing import Dict, Any

load_dotenv()


@dataclass
class LLMConfig:
    name: str
    base_url: str
    api_key: str
    model: str
    headers: Dict[str, str]


# Cau hinh Groq
GROQ = LLMConfig(
    name="groq",
    base_url="https://api.groq.com/openai/v1/chat/completions",
    api_key=os.getenv("GROQ_API_KEY", ""),
    model=os.getenv("GROQ_MODEL", "llama-3.3-70b-versatile"),
    headers={
        "Authorization": f"Bearer {os.getenv('GROQ_API_KEY', '')}",
        "Content-Type": "application/json"
    }
)

# Cau hinh vLLM Local
VLLM = LLMConfig(
    name="vllm",
    base_url=os.getenv("VLLM_BASE_URL", "http://localhost:3403") + "/v1/chat/completions",
    api_key="not-needed",  # vLLM local khong can API key
    model=os.getenv("VLLM_MODEL", "default"),
    headers={
        "Content-Type": "application/json"
    }
)


_PROVIDERS = {
    "groq": GROQ,
    "vllm": VLLM,
}


def get_provider(name: str = None) -> LLMConfig:
    if name is None:
        name = os.getenv("LLM_PROVIDER", "groq")
    
    name = name.lower()
    if name not in _PROVIDERS:
        raise ValueError(f"Provider '{name}' khong ton tai. Chon: {list(_PROVIDERS.keys())}")
    
    return _PROVIDERS[name]


DEFAULT = get_provider()
