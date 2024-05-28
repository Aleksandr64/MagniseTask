---
# Application Deployment with Docker Compose

This README provides instructions on how to deploy your application using Docker Compose, either through the terminal or an Integrated Development Environment (IDE).

## Prerequisites

Ensure you have the following installed on your system:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- An IDE with Docker support (optional), such as [Visual Studio Code](https://code.visualstudio.com/) with the Docker extension or [IntelliJ IDEA](https://www.jetbrains.com/idea/) with Docker integration.

## Directory Structure

Ensure your project directory includes the following files:

```
**MagniseTask/**
│
├── **docker-compose.yml**
└── **MagniseTask.Web/**
    └── **Dockerfile**
```

Navigate to the Project Directory:
Open your terminal and navigate to the directory containing your docker-compose.yml file:

```PowerShell
cd path/to/MagniseTask
```

Build and Start the Containers:
Use Docker Compose to build and start your containers:

```PowerShell
docker-compose up --build
```

---
# Testing Crypto Pairs Endpoint

This provides instructions on how to test the CryptoPare endpoint, which returns cryptocurrency pairs, using **Postman**. The endpoint is based on the CoinAPI /v1/symbols endpoint.


### Add Http Url
In Http Get request input url add this Url

```
http://localhost:5000/Crypto/CryptoPare
```

### Add Query Parameters:
Click on the Params tab and add the following parameter if you want to filter by symbol ID:

|Key|Value|
|---|-----|
|filterSymbolId|<symbol_id_here>|

### Examples of entering parameters

For testing endpoint you can use this params

#### With Exchanges
  - BITSTAMP_SPOT_BTC_USD - trading pair Bitcoin to USD on Bitstamp.
  - BINANCE_SPOT_ETH_USDT - trading pair Ethereum to Tether on Binance.
  - COINBASE_SPOT_LTC_BTC - trading pair Litecoin to Bitcoin on Coinbase.
  - KRAKEN_SPOT_XRP_EUR - trading pair Ripple to EUR on Kraken.
  - HITBTC_SPOT_BCH_USD - trading pair Bitcoin Cash to USD on HitBTC.
  
#### Without Exchanges
  - SPOT_BTC_USD - trading pair Bitcoin to USD.
  - SPOT_ETH_USDT - trading pair Ethereum to Tether.
  - SPOT_LTC_BTC - trading pair Litecoin to Bitcoin.

---
# Testing WebSocket Endpoint with Postman

This provides instructions on how to test the cryptoTrade WebSocket endpoint using **Postman**.

## Endpoint Details

### WebSocket URL:

```
ws://localhost:5000/cryptoTrade
```

### Protocol details
After connection you must send Protocol details

```
{
    "protocol": "json",
    "version": 1
}
```

### Message

When web socket start sent heartbeat you can send message for subscribe trade
In arguments you can change which trade you want get from CoinApi

#### Example 1
```
{
    "arguments": [["BITSTAMP_SPOT_BTC_USD$", "BITFINEX_SPOT_BTC_LTC$"]],
    "invocationId" : "0", 
    "target" : "Send", 
    "type" : 1
}
```

#### Example 1
```
{
    "arguments": [["BITSTAMP_SPOT_BTC_USD$", "BITFINEX_SPOT_BTC_LTC$"]],
    "invocationId" : "0", 
    "target" : "Send", 
    "type" : 1
}
```

### Disconect

For Disconect port you must send this message or Disconnect in Posman

```
{
    "arguments": [],
    "invocationId": "0",
    "target": "Disconnect",
    "type": 1
}
```

### Review the Responses:
Monitor the responses from the WebSocket server in the Postman interface. You should see updates or confirmations based on the messages you sent.

By following these steps, you can effectively test the cryptoTrade WebSocket endpoint using Postman, ensuring it handles subscriptions and disconnections as expected.
---
