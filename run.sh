#!/bin/bash

source .env.$1
ENV_FILE=.env.$1

docker compose \
    -p $PROJECT_NAME-$1 \
    --env-file $ENV_FILE \
    -f docker-compose.$ENV.yml up --build