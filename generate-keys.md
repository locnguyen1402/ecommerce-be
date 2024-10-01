# Generating Keys and Certificates

This guide will walk you through the steps to generate keys and certificates using the provided bash scripts and the `openssl.conf` configuration file.

## Prerequisites

- Ensure you have `openssl` installed on your system.
- Make sure you have the necessary permissions to execute the scripts.

## Steps to Generate Keys and Certificates

1. **Navigate to the directory containing the scripts**:

```sh
  cd src/deploy/local/certs
```

2. **Update config**

- Update configs in file `openssl.conf`

3. **Run by order**

```sh
  ./01-gen-ca-certs.sh
  ./02-gen-keys.sh
  ./03-gen-certs.sh
```
