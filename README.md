# Chat App Backend – Engineering Overview

## Overview

This repository contains the backend for a **real‑time chat system**.  
The focus is on **real‑time communication, authentication correctness, and clean backend structure**, rather than delivering a full end‑to‑end product.

The backend is designed to reliably support a modern frontend client while keeping responsibilities clearly separated.

***

## Core Capabilities

*   Real‑time messaging using **SignalR**
*   Authenticated, group‑based connections
*   Message persistence
*   Access / refresh token authentication

***

## Engineering Highlights

### SignalR‑Based Real‑Time Communication

*   Users are authenticated before establishing a SignalR connection
*   Chat rooms are represented using SignalR groups
*   Join, send, and disconnect flows are handled explicitly

This provides low‑latency updates without polling and keeps real‑time state centralized on the server.

***

### Authentication Integrated with Real‑Time Flow

*   JWT access tokens are used for authorization
*   Refresh tokens are stored and revoked server‑side
*   SignalR connections are authorized using the same auth mechanism

This ensures that real‑time connections are tied to valid user sessions and prevents stale or unauthorized access.

***

### Explicit Connection Lifecycle Handling

*   User validation on connect
*   Room membership management
*   Message persistence before broadcast
*   Cleanup on disconnect

This makes connection behavior predictable and avoids common real‑time edge cases.

***

### Service‑Driven Design

*   Business logic lives in application services
*   Hubs and controllers remain thin and focused

This improves readability, testability, and long‑term maintainability.

***

## Scope Note

Chat rooms are **predefined and seeded by design**.  
This is an intentional scope decision to keep the project focused on **real‑time logic and authentication**, rather than CRUD configuration.

Rooms can be made dynamic later without architectural changes.

***

## Summary

This project demonstrates:

*   Practical use of **SignalR** in authenticated scenarios
*   Correct handling of access and refresh tokens
*   Clean separation of real‑time infrastructure and business logic
*   Intentional scoping to focus on complex backend concerns

> This README is intentionally concise and intended for **engineers and reviewers**, not end users.
