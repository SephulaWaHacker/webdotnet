#!/usr/bin/env bash

echo -n "What is the name of the microservice: "
read -r
serviceName=$REPLY

cp -r service-templates $serviceName

cd $serviceName
sedCommand="sed -i "
if [[ "$OSTYPE" == "darwin"* ]]; then
  sedCommand="${sedCommand} '' "
fi

eval "$sedCommand 's/__CHART__/$serviceName/g' *.yaml"
eval "$sedCommand 's/__CHART__/$serviceName/g' *.md"

echo -n " "
echo -n "helm chart for $serviceName created"
echo -n " "
