Generative AI com Ollama
________________________________________
1. Pré-requisitos
✔ Instalar .NET 8 SDK
https://dotnet.microsoft.com/download
Verificar:
dotnet --version
________________________________________
✔ Instalar Docker Desktop
https://www.docker.com/products/docker-desktop/
Ativar:
•	WSL2 (Windows) 
•	Virtualization na BIOS (se necessário) 
________________________________________
✔ Instalar Ollama
Ollama
https://ollama.com/download
Verificar:
ollama --version
________________________________________
2. Baixar modelos do Ollama
Embeddings
ollama pull nomic-embed-text
Chat LLM
ollama pull qwen2.5:7b
________________________________________
Verificar:
ollama list
________________________________________
3. Subir Elasticsearch via Docker
Elasticsearch
docker run -d ^
  --name elastic ^
  -p 9200:9200 ^
  -p 9300:9300 ^
  -e "discovery.type=single-node" ^
  -e "xpack.security.enabled=false" ^
  docker.elastic.co/elasticsearch/elasticsearch:8.12.0
________________________________________
Testar:
http://localhost:9200
________________________________________
4. Criar solução .NET
dotnet new sln -n GenAI

dotnet new webapi -n GenAI.Api -f net8.0

dotnet sln add GenAI.Api/GenAI.Api.csproj
________________________________________
5. Instalar pacotes NuGet
Dentro da API:
  <ItemGroup>
    <PackageReference Include="Elasticsearch.Net" Version="7.17.5" />
    <PackageReference Include="itext7" Version="9.6.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.27" />
    <PackageReference Include="NEST" Version="7.17.5" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>
 ________________________________________
________________________________________
6. Rodar tudo localmente
Ordem correta:
1. Docker
docker start elastic
2. Ollama
ollama serve
3. API
dotnet run
________________________________________
7. Fluxo final que você construiu
PDF Upload
   ↓
Extract Text
   ↓
Chunking
   ↓
Embedding (Ollama)
   ↓
Index (Elasticsearch)
   ↓
Query Embedding
   ↓
Vector Search
   ↓
Context
   ↓
LLM (Qwen)
   ↓
Resposta

