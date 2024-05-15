#!/bin/bash

source .env.$1
ENV_FILE=.env.$1

docker compose \
    -p $PROJECT_NAME-$1 \
    -f compose.infra.yaml down

docker compose \
    -p $PROJECT_NAME-$1 \
    --env-file $ENV_FILE \
    -f compose.infra.yaml up --build --detach

# docker compose -f compose.infra.yaml down
# docker compose -f compose.infra.yaml up --build --detach
