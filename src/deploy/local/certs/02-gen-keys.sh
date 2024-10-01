#!/bin/bash

source $(dirname $0)/00-shared-info.sh

printf "\n\n3.1. Generate an infra private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/infrastructure-services/tls.key" 4096
export COMMON_NAME="infrastructure-services"
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/infrastructure-services/tls.key" \
        -out "${OUT_DIR}/infrastructure-services/tls.csr"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/infrastructure-services/tls.crt

# rabbitmq required key pem format
echo "Convert key to pem format, required by rabbitmq"
openssl rsa -in ${OUT_DIR}/infrastructure-services/tls.key -text > ${OUT_DIR}/infrastructure-services/key.pem

printf "\n\n3.2. Generate monitoring-services private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/monitoring-services/tls.key" 4096
export COMMON_NAME="monitoring-services"
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/monitoring-services/tls.key" \
        -out "${OUT_DIR}/monitoring-services/tls.csr"

printf "\n\n3.3. Generate api-gateway private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/api-gateway/tls.key" 4096

export COMMON_NAME="api-gateway"
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/api-gateway/tls.key" \
        -out "${OUT_DIR}/api-gateway/tls.csr"

printf "\n\n3.4. Generate api-services private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/api-services/tls.key" 4096

export COMMON_NAME="api-services"
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/api-services/tls.key" \
        -out "${OUT_DIR}/api-services/tls.csr"

printf "\n\n3.5. Generate api-services-data-protection private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/api-data-protection/encrypt.key" 4096

openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/api-data-protection/encrypt.key" \
        -out "${OUT_DIR}/api-data-protection/encrypt.csr"

openssl genrsa -out "${OUT_DIR}/api-data-protection/decrypt-01.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/api-data-protection/decrypt-01.key" \
        -out "${OUT_DIR}/api-data-protection/decrypt-01.csr"

openssl genrsa -out "${OUT_DIR}/api-data-protection/decrypt-02.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/api-data-protection/decrypt-02.key" \
        -out "${OUT_DIR}/api-data-protection/decrypt-02.csr"

printf "\n\n4.1. Generate identity-encrypt private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/identity-encrypt-signing/encrypt.key" 4096

export COMMON_NAME="identity"
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/identity-encrypt-signing/encrypt.key" \
        -out "${OUT_DIR}/identity-encrypt-signing/encrypt.csr"

printf "\n\n4.2. Generate identity-signing private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/identity-encrypt-signing/signing.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/identity-encrypt-signing/signing.key" \
        -out "${OUT_DIR}/identity-encrypt-signing/signing.csr"

printf "\n\n4.3. Generate identity-data-protection private key and certificate\n\n"
openssl genrsa -out "${OUT_DIR}/identity-data-protection/encrypt.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/identity-data-protection/encrypt.key" \
        -out "${OUT_DIR}/identity-data-protection/encrypt.csr"

openssl genrsa -out "${OUT_DIR}/identity-data-protection/decrypt-01.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/identity-data-protection/decrypt-01.key" \
        -out "${OUT_DIR}/identity-data-protection/decrypt-01.csr"

openssl genrsa -out "${OUT_DIR}/identity-data-protection/decrypt-02.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/identity-data-protection/decrypt-02.key" \
        -out "${OUT_DIR}/identity-data-protection/decrypt-02.csr"
