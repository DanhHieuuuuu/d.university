<p align="center">
  <img src="https://img.shields.io/badge/version-1.0.0-blue.svg" alt="Version"/>
  <img src="https://img.shields.io/badge/license-MIT-green.svg" alt="License"/>
  <img src="https://img.shields.io/badge/.NET-9.0-purple.svg" alt=".NET"/>
  <img src="https://img.shields.io/badge/Next.js-14-black.svg" alt="Next.js"/>
  <img src="https://img.shields.io/badge/Python-3.11-yellow.svg" alt="Python"/>
</p>

<h1 align="center">🎓 D.University</h1>

<p align="center">
  <strong>Hệ thống Quản lý Đại học Thông minh</strong><br/>
  Đồ án tốt nghiệp hệ Kỹ sư - Tích hợp AI & Microservices
</p>

<p align="center">
  <a href="#-tính-năng">Tính năng</a> •
  <a href="#-công-nghệ">Công nghệ</a> •
  <a href="#-cài-đặt">Cài đặt</a> •
  <a href="#-cấu-trúc-dự-án">Cấu trúc</a> •
  <a href="#-api-documentation">API</a>
</p>

---

## 📋 Tổng quan

**D.University** là hệ thống quản lý đại học toàn diện được xây dựng theo kiến trúc **Microservices**, tích hợp **AI/ML** để hỗ trợ tư vấn sinh viên và tìm kiếm thông minh. Hệ thống bao gồm các module quản lý nhân sự, đào tạo, sinh viên, ủy quyền và nhiều chức năng khác.

## ✨ Tính năng

### 🔐 Authentication & Authorization
- Đăng nhập/Đăng ký với JWT Authentication
- Phân quyền chi tiết theo Role-Based Access Control (RBAC)
- Quản lý phiên đăng nhập với Redis Cache

### 👥 Quản lý Nhân sự (HRM)
- Quản lý thông tin nhân viên, giảng viên
- Theo dõi KPI và đánh giá hiệu suất
- Quản lý ủy quyền công việc

### 🎓 Quản lý Đào tạo
- Quản lý chương trình đào tạo
- Theo dõi tiến độ học tập sinh viên
- Quản lý danh mục và khảo sát

### 🤖 AI Chatbot
- Tư vấn sinh viên tự động với RAG (Retrieval Augmented Generation)
- Tìm kiếm thông minh với xử lý ngôn ngữ tự nhiên
- Hỗ trợ đa ngôn ngữ (Tiếng Việt)

### 📊 Dashboard & Analytics
- Biểu đồ thống kê trực quan
- Báo cáo theo thời gian thực
- Export dữ liệu đa định dạng

### 🔔 Thông báo Real-time
- Push notification với SignalR
- Quản lý thông báo hệ thống
- Lịch sử thông báo

## 🛠 Công nghệ

### Backend (.NET 9)
| Công nghệ | Mô tả |
|-----------|-------|
| **ASP.NET Core 9** | Web API Framework |
| **Entity Framework Core** | ORM |
| **MediatR** | CQRS Pattern |
| **AutoMapper** | Object Mapping |
| **SQL Server** | Database |
| **Redis** | Distributed Caching |
| **MinIO/S3** | File Storage |
| **SignalR** | Real-time Communication |
| **Docker** | Containerization |

### Frontend (Next.js 14)
| Công nghệ | Mô tả |
|-----------|-------|
| **Next.js 14** | React Framework |
| **TypeScript** | Type Safety |
| **Redux Toolkit** | State Management |
| **Ant Design** | UI Component Library |
| **Tailwind CSS** | Utility-first CSS |
| **Axios** | HTTP Client |
| **SignalR Client** | Real-time Events |
| **SASS** | CSS Preprocessor |

### AI Service (Python)
| Công nghệ | Mô tả |
|-----------|-------|
| **FastAPI** | Python Web Framework |
| **PyTorch** | Machine Learning |
| **FAISS** | Vector Search |
| **Sentence Transformers** | Embedding Model |
| **Groq API** | LLM Integration |
| **Google GenAI** | AI Service |

## 📁 Cấu trúc dự án

```
d.university/
├── 📂 fe/                          # Frontend (Next.js)
│   ├── app/                        # App Router
│   │   ├── (auth)/                 # Authentication pages
│   │   ├── (home)/                 # Main application
│   │   │   ├── delegation/         # Ủy quyền
│   │   │   ├── hrm/                # Quản lý nhân sự
│   │   │   ├── student/            # Quản lý sinh viên
│   │   │   ├── training/           # Đào tạo
│   │   │   ├── kpi/                # KPI
│   │   │   └── survey/             # Khảo sát
│   │   ├── manager/                # Admin management
│   │   └── chatbot/                # AI Chatbot interface
│   ├── src/                        # Source files
│   │   ├── components/             # Reusable components
│   │   ├── services/               # API services
│   │   ├── store/                  # Redux store
│   │   └── styles/                 # Global styles
│   └── public/                     # Static assets
│
├── 📂 be/                          # Backend (.NET)
│   ├── Services/
│   │   ├── Auth/                   # Authentication Service
│   │   │   ├── D.Auth.API/         # API Layer
│   │   │   ├── D.Auth.Application/ # Application Layer
│   │   │   ├── D.Auth.Domain/      # Domain Layer
│   │   │   └── D.Auth.Infrastructure/
│   │   ├── Core/                   # Core Service
│   │   │   ├── D.Core.API/
│   │   │   ├── D.Core.Application/
│   │   │   ├── D.Core.Domain/
│   │   │   └── D.Core.Infrastructure/
│   │   └── Shared/                 # Shared Libraries
│   ├── Library/                    # Base Libraries
│   ├── Chatbot/                    # RAG Chatbot (Python)
│   └── docker-compose.yml
│
└── 📂 AI/                          # AI Search Service
    ├── core/
    ├── models/
    ├── services/
    └── main.py
```

## 🚀 Cài đặt

### Yêu cầu hệ thống
- **Node.js** >= 18.x
- **.NET SDK** >= 9.0
- **Python** >= 3.11
- **SQL Server** 2019+
- **Redis** (hoặc Upstash Redis)
- **Docker** (tùy chọn)

### 1. Clone Repository

```bash
git clone https://github.com/DanhHieuuuuu/d.university.git
cd d.university
```

### 2. Cài đặt Frontend

```bash
cd fe
npm install
cp .env.example .env    # Cấu hình biến môi trường
npm run dev
```

Frontend chạy tại: `http://localhost:3077`

### 3. Cài đặt Backend

```bash
cd be/Services/Core/D.Core.API
dotnet restore
dotnet run
```

**Chạy Migration:**
```bash
cd Services/Core/D.Core.Domain
dotnet ef migrations add InitDatabase --context CoreDBContext --startup-project ../D.Core.API
dotnet ef database update --context CoreDBContext --startup-project ../D.Core.API
```

### 4. Cài đặt AI Service

```bash
cd AI
python -m venv venv
.\venv\Scripts\activate      # Windows
pip install -r requirements.txt
uvicorn main:app --reload --port 8001
```

### 5. Cài đặt Chatbot Service

```bash
cd be/Chatbot
python -m venv venv
.\venv\Scripts\activate
pip install -r requirements.txt
cp .env.example .env         # Thêm GROQ_API_KEY
uvicorn app.main:app --reload --port 8000
```

### 6. Chạy với Docker

```bash
cd be
docker-compose up -d
```

## 🔗 API Documentation

### Service Endpoints

| Service | URL | Mô tả |
|---------|-----|-------|
| **Auth API** | `http://localhost:10000` | Authentication Service |
| **Core API** | `http://localhost:10001` | Core Business Service |
| **Chatbot API** | `http://localhost:8000` | RAG Chatbot Service |
| **AI Search API** | `http://localhost:8001` | AI Search Service |
| **Frontend** | `http://localhost:3077` | Web Application |

### Chatbot API

```bash
# Chat với AI
POST /api/chat
{
  "message": "Điểm học kỳ 2 của tôi là bao nhiêu?",
  "conversation_history": []
}

# Lấy định hướng học tập
GET /api/orientation

# Rebuild vector index
POST /api/rebuild-index
```

### AI Search API

```bash
# Tìm kiếm thông minh với AI
POST /api/ai-search
{
  "query": "Tìm tất cả ủy quyền trong tháng 1"
}
```

## 🏗 Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                        CLIENT LAYER                              │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │                    Next.js 14 (React)                     │   │
│  │         Redux Toolkit • Ant Design • Tailwind CSS         │   │
│  └──────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                       API GATEWAY                                │
└─────────────────────────────────────────────────────────────────┘
                              │
          ┌───────────────────┼───────────────────┐
          ▼                   ▼                   ▼
┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐
│   Auth Service  │  │   Core Service  │  │   AI Services   │
│    (.NET 9)     │  │    (.NET 9)     │  │    (Python)     │
│                 │  │                 │  │                 │
│  • JWT Auth     │  │  • HRM          │  │  • Chatbot RAG  │
│  • RBAC         │  │  • Training     │  │  • AI Search    │
│  • Session      │  │  • Student      │  │  • Embedding    │
└────────┬────────┘  └────────┬────────┘  └────────┬────────┘
         │                    │                    │
         ▼                    ▼                    ▼
┌─────────────────────────────────────────────────────────────────┐
│                      DATA LAYER                                  │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │  SQL Server  │  │    Redis     │  │  MinIO/S3    │          │
│  │  (Database)  │  │   (Cache)    │  │   (Files)    │          │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
└─────────────────────────────────────────────────────────────────┘
```

## 👥 Team

| Thành viên | Vai trò |
|------------|---------|
| **DanhHieu** | Project Owner |
| **KhoaPD** | Developer |

## 📄 License

Dự án này được phát hành dưới giấy phép **MIT License**.

---

<p align="center">
  Made with ❤️ by D.University Team
</p>

<p align="center">
  <a href="https://github.com/DanhHieuuuuu/d.university">
    <img src="https://img.shields.io/github/stars/DanhHieuuuuu/d.university?style=social" alt="GitHub Stars"/>
  </a>
</p>
