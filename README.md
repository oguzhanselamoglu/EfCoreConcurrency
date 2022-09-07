## Veri Tabanı
* veri tabanı olarak Docker ortamında sql server kullanıldı
* Docker Sql Container için Portainer dan faydalanabilir kurulumu aşağıdaki gibi

## Portainer
 ```
 docker volume create portainer_data
 docker run -d -p 8000:8000 -p 9443:9443 --name portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce:latest
 ```

