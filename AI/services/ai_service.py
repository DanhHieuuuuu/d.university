import json
from core.config import client

def extract_conditions(query: str) -> dict:
    prompt = f"""
Bạn là AI trích xuất điều kiện tìm kiếm cho hệ thống ĐOÀN VÀO.

Các field hợp lệ:
- id (int)
- code (string)
- name (string)
- status (int)
- id_phong_ban (int)
- id_staff_reception (int)
- from_request_date (yyyy-mm-dd)
- to_request_date (yyyy-mm-dd)
- from_reception_date (yyyy-mm-dd)
- to_reception_date (yyyy-mm-dd)

Chỉ trả về JSON, KHÔNG giải thích.

Câu hỏi người dùng:
{query}
"""

    response = client.models.generate_content(
        model="gemini-2.5-flash",
        contents=prompt
    )

    text = response.text.strip()

    # phòng trường hợp Gemini bọc ```json
    if text.startswith("```"):
        text = text.replace("```json", "").replace("```", "").strip()

    return json.loads(text)
