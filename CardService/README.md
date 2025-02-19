# CardService - Deployment Guide

## Table of Contents
- [Introduction](#introduction)
- [Project Structure](#project-structure)
- [Local Development](#local-development)
- [Docker Setup](#docker-setup)
- [Kubernetes Deployment](#kubernetes-deployment)
- [Azure AKS Deployment](#azure-aks-deployment)

## Introduction
This document provides a comprehensive guide on how to set up, deploy, and run the `CardService` microservice locally, in Docker, Kubernetes, and Azure Kubernetes Service (AKS).

## Project Structure
The project follows a structured microservices architecture:

```
CardService/
│-- src/                             # Source code
│   ├── CardService.Api/             # API Layer
│   ├── CardService.Application/     # Application Layer (CQRS, Services, Handlers)
│   ├── CardService.Domain/          # Domain Entities and Business Logic
│   ├── CardService.Infrastructure/  # Infrastructure Layer (Persistence, Services)
│-- tests/                           # Unit and Integration Tests
│   ├── CardService.Tests/
│-- k8s/                             # Kubernetes Configuration
│   ├── deployment.yaml
│   ├── service.yaml
|-- docker-compose.yml               # Docker Compose Configuration
│-- Dockerfile                       # Docker Configuration
│-- README.md                        # Documentation
```

## Local Development
### Prerequisites
Ensure you have the following installed:
- .NET 8 SDK
- Visual Studio Code (or any preferred IDE)
- Docker
- Kubernetes (Minikube, k3s, or a local cluster setup)
- Azure CLI (for Azure deployment)

### Running Locally
1. Clone the repository:
   ```sh
   git clone https://github.com/eckopage/madiff-card-action-generator.git
   cd CardService/src
   ```
2. Restore dependencies and build the project:
   ```sh
   dotnet restore
   dotnet build
   ```
3. Run the application:
   ```sh
   dotnet run --project CardService.Api
   ```
4. The API should be available at `http://localhost:5176` (or a configured port).

## Docker Setup
### Building and Running Docker Image
1. Navigate to the source folder:
   ```sh
   cd CardService/src
   ```
2. Build the Docker image:
   ```sh
   docker build -t cardservice:latest .
   ```
3. Run the container:
   ```sh
   docker run -p 8080:80 cardservice:latest
   ```
4. Access the API at `http://localhost:8080`.

## Kubernetes Deployment
### Deploying Locally with Minikube
1. Start Minikube:
   ```sh
   minikube start
   ```
2. Create a Kubernetes deployment:
   ```sh
   kubectl apply -f k8s/deployment.yaml
   ```
3. check pods
   ```sh
   kubectl get pods
   ```
4. Expose the service:
   ```sh
   kubectl expose deployment cardservice --type=LoadBalancer --name=cardservice-service
   ```
5. Retrieve the service URL:
   ```sh
   minikube service cardservice-service --url
   ```

## Azure AKS Deployment
### Setting Up AKS
1. Login to Azure:
   ```sh
   az login
   ```
2. Create a resource group:
   ```sh
   az group create --name CardServiceGroup --location eastus
   ```
3. Create an AKS cluster:
   ```sh
   az aks create --resource-group CardServiceGroup --name CardServiceCluster --node-count 2 --enable-addons monitoring --generate-ssh-keys
   ```
4. Connect to the cluster:
   ```sh
   az aks get-credentials --resource-group CardServiceGroup --name CardServiceCluster
   ```
5. Deploy the application:
   ```sh
   kubectl apply -f k8s/deployment.yaml
   ```
6. Expose the service:
   ```sh
   kubectl expose deployment cardservice --type=LoadBalancer --name=cardservice-service
   ```
7. Get the external IP:
   ```sh
   kubectl get services
   ```
