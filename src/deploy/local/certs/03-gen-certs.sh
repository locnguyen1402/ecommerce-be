#!/bin/bash

source $(dirname $0)/00-shared-info.sh

printf "\n\n3.1. Generate an infra private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:redis,DNS:rabbitmq") \
        -in "${OUT_DIR}/infrastructure-services/tls.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/infrastructure-services/tls.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/infrastructure-services/tls.crt
openssl rsa -in ${OUT_DIR}/infrastructure-services/tls.key -text > ${OUT_DIR}/infrastructure-services/key.pem

printf "\n\n3.2. Generate monitoring-services private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:otel,DNS:loki,DNS:mimir,DNS:tempo,DNS:grafana") \
        -in "${OUT_DIR}/monitoring-services/tls.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/monitoring-services/tls.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/monitoring-services/tls.crt

printf "\n\n3.3. Generate api-gateway private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:api-gateway") \
        -in "${OUT_DIR}/api-gateway/tls.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/api-gateway/tls.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/api-gateway/tls.crt

printf "\n\n3.4. Generate api-services private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:blog,DNS:catalog,DNS:cms,DNS:customer,DNS:identity,DNS:notification,DNS:object-storage,DNS:ordering,DNS:store,DNS:promotion,DNS:payment,DNS:delivery,DNS:loyalty") \
        -in "${OUT_DIR}/api-services/tls.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/api-services/tls.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/api-services/tls.crt

printf "\n\n3.5. Generate api-services-data-protection private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:blog,DNS:catalog,DNS:cms,DNS:customer,DNS:identity,DNS:notification,DNS:object-storage,DNS:ordering,DNS:store,DNS:payment,DNS:delivery") \
        -in "${OUT_DIR}/api-data-protection/encrypt.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/api-data-protection/encrypt.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/api-data-protection/encrypt.crt

openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:blog,DNS:catalog,DNS:cms,DNS:customer,DNS:identity,DNS:notification,DNS:object-storage,DNS:ordering,DNS:store,DNS:payment,DNS:delivery") \
        -in "${OUT_DIR}/api-data-protection/decrypt-01.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/api-data-protection/decrypt-01.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/api-data-protection/decrypt-01.crt

openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:blog,DNS:catalog,DNS:cms,DNS:customer,DNS:identity,DNS:notification,DNS:object-storage,DNS:ordering,DNS:store,DNS:payment,DNS:delivery") \
        -in "${OUT_DIR}/api-data-protection/decrypt-02.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/api-data-protection/decrypt-02.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/api-data-protection/decrypt-02.crt

printf "\n\n4.1. Generate identity-encrypt private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:identity") \
        -in "${OUT_DIR}/identity-encrypt-signing/encrypt.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/identity-encrypt-signing/encrypt.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/identity-encrypt-signing/encrypt.crt

printf "\n\n4.2. Generate identity-signing private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:identity") \
        -in "${OUT_DIR}/identity-encrypt-signing/signing.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/identity-encrypt-signing/signing.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/identity-encrypt-signing/signing.crt

printf "\n\n4.3. Generate identity-data-protection private key and certificate\n\n"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:identity") \
        -in "${OUT_DIR}/identity-data-protection/encrypt.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/identity-data-protection/encrypt.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/identity-data-protection/encrypt.crt

openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:identity") \
        -in "${OUT_DIR}/identity-data-protection/decrypt-01.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/identity-data-protection/decrypt-01.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/identity-data-protection/decrypt-01.crt

openssl x509 -req -days ${EXP_DAYS} \
        -extfile <(printf "subjectAltName=DNS:identity") \
        -in "${OUT_DIR}/identity-data-protection/decrypt-02.csr" \
        -CA "${OUT_DIR}/intermediate.crt" \
        -CAkey "${OUT_DIR}/intermediate.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/identity-data-protection/decrypt-02.crt"

cat ${OUT_DIR}/intermediate.crt >> ${OUT_DIR}/identity-data-protection/decrypt-02.crt
