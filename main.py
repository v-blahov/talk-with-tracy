from fastapi import FastAPI, HTTPException, Request
import requests
from pydantic import BaseModel
from typing import Optional
app = FastAPI()

class MessagePayload(BaseModel):
    message: str
    thread_id: Optional[int] = None

@app.post("/chat")
def send_message(payload: MessagePayload):
    url = "https://api.tereza.ai/chat"

    response = requests.post(url, json=payload.dict(), headers={"Content-Type": "application/json"})

    if response.status_code == 200:
        return response.json()
    raise HTTPException(status_code=response.status_code, detail={"error": "Failed to send message", "details": response.text})


