
## Ef Core Concurrency
* Birden fazla kullanıcının, aynı kayıt üzerinde eş zamanlı yaptıgı güncelleme işleminde veri bütünlüğünün korunması için Ef Core tarafında ele alabileceğimiz durumlar
* Pessimistic concurrency control (xlock) ilgili satırı okuma anında kitler, serbest bırakılana kadar başka kimse okuma yapamaz. Tavsiye edilmiyor, performance sıkıntıları var
* Optimistic concurrency control (2 kullanım şekli var ConcurrencyToken ve RowVersion)

## Fluent Api
 ```
   modelBuilder.Entity<Product>().Property(x => x.RowVersion).IsRowVersion();
 ```

## Attribute olarak kullanımı
 ```
  [TimeStamp]
  public byte[] RowVersion { get; set; }
 ```

## Veri Tabanı
* Veri tabanı olarak Docker ortamında sql server kullanıldı
* Docker Sql Container için Portainer dan faydalanabilir kurulumu aşağıdaki gibi

## Portainer
 ```
 docker volume create portainer_data
 docker run -d -p 8000:8000 -p 9443:9443 --name portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce:latest
 ```

