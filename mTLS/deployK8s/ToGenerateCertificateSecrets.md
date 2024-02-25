Secret to cert DNS applicationA
```sh
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout serverA.key -out serverA.crt -subj "/CN=appA.example.com/O=appA.example.com"
```
```sh
kubectl create secret tls app-a-tls --key serverA.key --cert serverA.crt
```

Make CA secret and pfx to mTLS
```sh
openssl req -x509 -sha256 -newkey rsa:4096 -keyout ca.key -out ca.crt -days 356 -nodes -subj '/CN=My Cert Authority'
```
```sh
kubectl create secret generic ca-secret --from-file=ca.crt=ca.crt
```
```sh
openssl req -new -newkey rsa:4096 -keyout clientB.key -out clientB.csr -subj "/CN=my client B cert"
```
```sh
openssl x509 -req -sha256 -days 365 -in clientB.csr -CA ca.crt -CAkey ca.key -set_serial 02 -out clientB.crt
```
```sh
openssl pkcs12 -export -out clientB.pfx -inkey clientB.key -in clientB.crt
```
```sh
kubectl create secret generic ca-secret --from-file=clientB.pfx=clientB.pfx
```

Secret to cert DNS applicationB
```sh
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout serverB.key -out serverB.crt -subj "/CN=appB.example.com/O=appB.example.com"
```
```sh
kubectl create secret tls app-b-tls --key serverB.key --cert serverB.crt
```

