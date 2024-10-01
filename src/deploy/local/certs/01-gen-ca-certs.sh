#!/bin/bash

source $(dirname $0)/00-shared-info.sh

rm -rf ${OUT_DIR}
mkdir -p ${OUT_DIR}/{infrastructure-services,monitoring-services,api-gateway,api-services,api-data-protection,identity-encrypt-signing,identity-data-protection}

printf "1. Generate a root CA cert-key pair\n\n"
openssl genrsa -out "${OUT_DIR}/root.key" 4096

export COMMON_NAME="root"
openssl req -new -x509 -nodes -days ${EXP_DAYS} \
        -config ./openssl.conf -extensions v3_ca \
        -key "${OUT_DIR}/root.key" \
        -out "${OUT_DIR}/root.crt"

printf "2. Generate a intermediate CA cert-key pair\n\n"
openssl genrsa -out "${OUT_DIR}/intermediate.key" 4096
openssl req -new -nodes \
        -config ./openssl.conf \
        -key "${OUT_DIR}/intermediate.key" \
        -out "${OUT_DIR}/intermediate.csr"

export COMMON_NAME="intermediate"
openssl x509 -req -days ${EXP_DAYS} \
        -extfile /etc/ssl/openssl.cnf -extensions v3_ca  \
        -in "${OUT_DIR}/intermediate.csr" \
        -CA "${OUT_DIR}/root.crt" \
        -CAkey "${OUT_DIR}/root.key" \
        -CAcreateserial \
        -out "${OUT_DIR}/intermediate.crt"

cat ${OUT_DIR}/intermediate.crt > ${OUT_DIR}/chain.crt
cat ${OUT_DIR}/root.crt >> ${OUT_DIR}/chain.crt
