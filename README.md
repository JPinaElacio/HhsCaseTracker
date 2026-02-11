# 🏥 HHS Case Tracking System

A full-stack web application designed to simulate a Health & Human Services case management system. This project demonstrates enterprise-style application development, backend API design, database integration, and frontend–backend communication.

---

## 🚀 Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (LocalDB)
- RESTful API design
- Structured logging and error handling

### Frontend
- React
- TypeScript
- Vite
- Axios for API communication

---

## 🧩 Features

- Create, read, update, and delete cases (full CRUD)
- Real-time frontend updates with React state management
- Backend data validation using Data Annotations
- Structured logging for operational monitoring
- Error handling for stable API responses
- Client–server architecture

---

---

## ⚙️ How to Run the Project

### 📌 Prerequisites
Make sure you have installed:

- .NET 8 SDK  
- Node.js (v18+ recommended)  
- SQL Server LocalDB (included with Visual Studio)

---

### 🖥 Run the Backend (ASP.NET Core API)

1. Open the solution in **Visual Studio**
2. Set `HhsCaseTracker.Api` as the Startup Project
3. Click **Run ▶**

### Run the Frontend 

1. Open a separate terminal
2. Next cd hhs-case-ui
3. npm install
4. npm run dev

### To stop the system (Backend and Frontend)
1. CTRL = C 

---

---

## 🔍 Key Technical Highlights

- Designed and implemented RESTful API endpoints following HTTP standards  
- Integrated Entity Framework Core for database access and schema migrations  
- Implemented structured logging and error handling for operational visibility  
- Applied backend validation to ensure data integrity  
- Connected a React frontend to a .NET backend using Axios for API calls  
- Managed frontend state updates after create, edit, and delete operations  
- Followed a client–server architecture, separating UI and business logic

---

## 🧠 Challenges & Learning

This project involved troubleshooting real-world full-stack integration issues such as:

- Resolving HTTPS vs HTTP API connection problems  
- Configuring CORS to allow frontend–backend communication  
- Debugging TypeScript module resolution errors  
- Handling API error responses gracefully in the UI  
- Understanding how backend logging supports application monitoring  

These challenges improved my understanding of full-stack architecture and system reliability.


