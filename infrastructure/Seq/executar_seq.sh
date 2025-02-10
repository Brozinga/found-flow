PH=$(echo 'admin' | docker run --rm -i datalust/seq config hash)

mkdir -p seq_data

docker run \
  --name seq \
  -d \
  --restart unless-stopped \
  -e ACCEPT_EULA=Y \
  -e SEQ_FIRSTRUN_ADMINPASSWORDHASH="$PH" \
  -v seq_data:/data \
  -p 5341:80 \
  datalust/seq