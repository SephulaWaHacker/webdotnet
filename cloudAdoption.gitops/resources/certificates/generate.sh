#!/bin/bash

source ./dns-dev.config

SUBDOMAIN=hackathon
DOMAIN=ttd-blue.azure.bmw.cloud
MAILBOX=cloud-adoption-certificate-requests@list.bmw.com

## CERTIFICATE REQUEST:
openssl req -new -newkey rsa:2048 -nodes -sha256 -keyout ${SUBDOMAIN}.key -out ${SUBDOMAIN}.csr -subj  "/C=DE/ST=Bavaria/L=Munich/O=Bayerische Motoren Werke AG/OU=PKI Services/CN=${SUBDOMAIN}.${DOMAIN}/emailAddress=${MAILBOX}" -config <(
cat <<-EOF
[req]
default_bits = 2048
default_md = sha256
req_extensions = req_ext
distinguished_name = dn
[ dn ]
[ req_ext ]
subjectAltName = @alt_names
[alt_names]
$DNS
EOF
)
