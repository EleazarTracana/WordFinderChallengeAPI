# WordFinderAPI

## QU Beyond Word Finder API

### Development Environment Setup


Before you can build this project, you must install and configure the following dependencies on your machine:

1. Docker - You can download and install Docker from [docker.com](https://www.docker.com/products/docker-desktop).

### Running the Local Environment


To run the local development environment, follow these steps:

1. Open a terminal and navigate to the root folder of the project.

2. Run the following command to start the development environment using Docker Compose:

   ```bash
   docker-compose -f run-development.yml up -d
### Generating Auth Tokens:

Before you can interact with the WordFinderAPI, you need to generate an authorization token from our Keycloak service. Follow these steps to generate the token:

1. Replace the placeholders {USERNAME} and {PASSWORD} with your Keycloak username and password in the following curl command:

   ```bash
   curl --location --request POST 'http://localhost:8180/realms/wordfinder/protocol/openid-connect/token' \
    --header 'Content-Type: application/x-www-form-urlencoded' \
    --data-urlencode 'grant_type=password' \
    --data-urlencode 'client_id=word-finder-client' \
    --data-urlencode 'username={USERNAME}' \
    --data-urlencode 'password={PASSWORD}' \
    --data-urlencode 'scope=openid'
   
### System Overview:

The system contains the following services:

1. WordFinderAPI: A .NET 7 Minimal API for handling the WordFinder Logic for the QU Beyond Challenge.
2. Redis: A Redis Database for improving the performance of the WordFinder API.
3. Keycloak:  An Authorization Service for securing the WordFinder API endpoints through custom policies.

![WordFinderOverview](https://github.com/EleazarTracana/WordFinderAPI/assets/48774395/05e98476-4b0c-4472-9039-6ed76eb88228)
