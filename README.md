# WordFinderAPI

## Description 

WordFinderAPI 🚀 is a Dockerized .NET 7 Minimal API crafted to tackle 2D string searches 🔍, as part of the QU Beyond developer challenge. We've containerized everything 🐳 to make it super easy for the awesome engineers reviewing our solution! 😎

### System Overview:

Here's a quick look at what's inside:

1. WordFinderAPI: Handles the WordFinder logic for QU Beyond Challenge. 🧙‍♂️
2. Redis: Boosts WordFinderAPI's performance. 🚀
3. Keycloak: Keeps WordFinderAPI secure with custom policies. 🔒

###  WordFinder Request Example:

![WordFinderOverview](https://github.com/EleazarTracana/WordFinderAPI/assets/48774395/05e98476-4b0c-4472-9039-6ed76eb88228)

### Development Environment Setup 🛠️

To get things rolling, make sure you've got these tools in your toolkit:

1. Docker - You can download and install Docker from [docker.com](https://www.docker.com/products/docker-desktop).

### Running the Local Environment 🚀

Ready to fire things up locally? Just follow these simple steps:

1. Open a terminal and navigate to the root folder of the project.

2. Now, let's kickstart the local development environment with this command:

   ```bash
   docker-compose -f run-development.yml up -d

That's it! You're off to the races! 🏁
### Generating Auth Tokens 🚀

Time to get access! To interact with the WordFinderAPI, you'll need an authorization token from our Keycloak service. It's easy, just follow these steps: 😎

1. Replace the placeholders {USERNAME} and {PASSWORD} with your Keycloak username and password in the following curl command:

   ```bash
   curl --location --request POST 'http://localhost:8180/realms/wordfinder/protocol/openid-connect/token' \
    --header 'Content-Type: application/x-www-form-urlencoded' \
    --data-urlencode 'grant_type=password' \
    --data-urlencode 'client_id=word-finder-client' \
    --data-urlencode 'username={USERNAME}' \
    --data-urlencode 'password={PASSWORD}' \
    --data-urlencode 'scope=openid'

