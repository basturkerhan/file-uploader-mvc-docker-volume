### file-uploader-mvc-docker-volume
Docker volume ile bir container içerisinde kalıcı alan oluşturmayı denemek için MVC mimarisi ile basit bir resim yükleme uygulaması geliştirdim.

#### 1- Docker Volume Oluşturma Cmd Kodu
docker volume create images

#### 2- Docker Build Cmd Kodu (Dockerfile dosyasının bulunduğu dizinden çalışacak cmd kodu)
docker build -t file-uploader-mvc .

#### 3- Docker Container Oluşturma Cmd Kodu
docker run -d -p 5000:4500 --name file-uploader-container -v images:/app/wwwroot/images file-uploader-mvc
