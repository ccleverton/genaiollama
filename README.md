Generative AI com Ollama

### 1. Pré-requisitos
Instalar .NET 8 SDK \
https://dotnet.microsoft.com/download \
Verificar: \
dotnet --version


### 2. Instalar Docker Desktop

https://www.docker.com/products/docker-desktop \
Ativar: WSL, Virtualization na BIOS (se necessário) 


### 3. Instalar Ollama
https://ollama.com/download \
Após instalação: \
ollama --version


### 4. Baixar modelos do Ollama

Embeddings\
ollama pull nomic-embed-text

Chat LLM\
ollama pull qwen2.5:7b

após downloads executar:\
ollama list


### 5. Subir Elasticsearch via Docker

docker run -d ^\
  --name elastic ^\
  -p 9200:9200 ^\
  -p 9300:9300 ^\
  -e "discovery.type=single-node" ^\
  -e "xpack.security.enabled=false" ^\
  docker.elastic.co/elasticsearch/elasticsearch:8.12.0


Após subir o container verificar no browser:\
http://localhost:9200


### 6. Dependências do projeto
Dentro da API: \ 
    "Elasticsearch.Net" Version="7.17.5"  \
    "itext7" Version="9.6.0"  \
    "NEST" Version="7.17.5" 
  
 
### 7. Comandos para rodar tudo localmente
Ordem correta:\
docker start elastic\
ollama serve\
dotnet run
