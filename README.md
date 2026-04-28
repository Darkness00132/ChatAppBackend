# 💬 Real-Time Chat System

A lightweight real-time chat backend built with ASP.NET Core SignalR featuring authentication, chat rooms, and persistent messaging.

The project focuses on real-time communication, secure authentication, and clean backend architecture.

---

## 🚀 Features

* JWT Authentication + Refresh Tokens
* Real-time messaging using SignalR
* Chat rooms (Groups-based messaging)
* Join / Leave room handling
* Message persistence
* User tracking inside chat sessions

---

## 🏗️ Architecture

Follows Clean Architecture:

* Application → business logic (services)
* Domain → core entities & rules
* Infrastructure → data access & external services

Uses:

* Repository Pattern
* Unit of Work
* Dependency Injection

---

## ⚙️ Core Logic Highlights

### 🔐 Authentication Flow

* User logs in and receives JWT access token
* Refresh token used for session renewal
* Secure identity-based access to SignalR hub

---

### 💬 Real-Time Chat System

Users connect to SignalR hub and:

* Join chat room via group
* Send messages in real-time
* Receive broadcast messages instantly

---

### 👥 Room Management

* Users join/leave rooms dynamically
* Automatic group assignment in SignalR
* System messages for join/leave events

---

### 💾 Message Handling

* Messages stored in database
* Sent messages broadcast to group
* Supports per-room message history retrieval

---

## 🛠️ Tech Stack

* ASP.NET Core
* SignalR
* Entity Framework Core
* ASP.NET Identity
* JWT

---

## 🧪 Run

```bash id="run-steps"
git clone https://github.com/your-username/chat-system.git
cd chat-system
dotnet restore
dotnet run
```

---

## 📌 Notes

This project focuses on:

* Real-time communication using SignalR
* Secure authentication flow (JWT + Refresh Tokens)
* Clean backend architecture
* Group-based messaging system
