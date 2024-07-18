<h1>Folder Sync Service</h1>

   <h2>Açıklama</h2>
    <p>Folder Sync Service, C# kullanılarak .NET Core ile geliştirilmiş çapraz platform destekli bir uygulamadır. İki klasörü dosya değişiklikleri için izler ve bunları senkronize eder.Loglama için Serilog kullanır ve Windows, macOS ve Linux platformlarında çalışabilir.</p>

  <h2>Başlangıç</h2>
    <p>Kullanmaya başlamanız için aşağıdaki adımları takip edebilirsiniz:</p>

  <h3>Ön Koşullar</h3>
    <ul>
        <li>.NET SDK 8 (veya en güncel .NET sürümü)</li>
        <li>Windows, macOS veya Linux işletim sistemi</li>
    </ul>

  <h3>Kurulum</h3>
    <ol>
        <li><strong>Depoyu Klonlayın:</strong></li>
        <pre><code>git clone https://github.com/username/document-sync-service.git</code></pre>

   <li><strong>Proje Klasörüne Geçin:</strong></li>
        <pre><code>cd document-sync-service</code></pre>

  <li><strong>Projeyi Derleyin:</strong></li>
        <pre><code>dotnet build</code></pre>

   <li><strong>Projeyi Yayınlayın:</strong></li>
        <pre><code>dotnet publish -c Release -o ./publish</code></pre>

  <li><strong>appsettings.json'u Yapılandırın:</strong> <code>appsettings.json</code> dosyasını kaynak , hedef ve log klasörlerinizle güncelleyin:</li>
        <pre><code>
{
  "FileSettings": {
    "SourceDirectory": "C:\\Dosya\\Yolu\\KaynakDosya",
    "TargetDirectory": "C:\\Dosya\\Yolu\\HedefDosya"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": Dosya\\Yolu\\Log\\log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
        </code></pre>
    </ol>

  <h3>Platforma Özgü Yapılandırma</h3>
    <h4>Windows</h4>
    <ol>
        <li><strong>Servisi Kurun:</strong></li>
        <pre><code>sc create DocumentSyncService binPath="C:\\Dosya\\Yolu\\DocumentService.exe"</code></pre>

  <li><strong>Servisi Başlatın:</strong></li>
        <pre><code>sc start DocumentSyncService</code></pre>
    </ol>

 <h4>macOS ve Linux</h4>
    <ol>
        <li><strong>.NET SDK'yı Kurun:</strong></li>
        <ul>
            <li><strong>macOS:</strong></li>
            <pre><code>brew install --cask dotnet-sdk</code></pre>

  <li><strong>Linux (Ubuntu):</strong></li>
            <pre><code>sudo apt-get install dotnet-sdk</code></pre>
        </ul>

  <li><strong>Servisi Yayınlayın ve Çalıştırın:</strong> Yayınlanan dosyaları seçtiğiniz bir dizine taşıyın</li>

  <li><strong>Bir systemd hizmet dosyası oluşturun:</strong> <code>/etc/systemd/system/document-sync-service.service</code></li>
        <pre><code>
[Unit]
Description=Folder Sync Service

[Service]
ExecStart=/usr/local/bin/document-sync-service/DocumentService
Restart=always
User=nobody
Group=nogroup

[Install]
WantedBy=multi-user.target
        </code></pre>

  <li><strong>Servisi Etkinleştirin:</strong></li>
        <pre><code>sudo systemctl enable document-sync-service
sudo systemctl start document-sync-service</code></pre>
    </ol>

<h2>Kullanım</h2>
    <p>Folder Sync Service, belirtilen SourceDirectory'deki dosya değişikliklerini (oluşturma, güncelleme, yeniden adlandırma, silme) izler ve bu değişiklikleri TargetDirectory'ye senkronize eder. Tüm işlemleri belirtilen log dosyasına kaydeder.</p>

<h2>Katkıda Bulunma</h2>
    <p>Projeyi forklayarak katkıda bulunmaktan çekinmeyin :)</p>

<h2>Lisans</h2>
    <p>Bu proje MIT Lisansı altında lisanslanmıştır - ayrıntılar için LICENSE dosyasına bakın.</p>
</br>
</br>
<h1>English</h1>

<h1>Folder Sync Service</h1>

 <h2>Description</h2>
    <p>Document Sync Service is a cross-platform  service written in C# using .NET Core. It monitors two directories for file changes and synchronizes them. The service utilizes Serilog for logging and supports running on Windows, macOS, and Linux platforms.</p>

<h2>Getting Started</h2>
    <p>To get started with Document Sync Service, follow these steps:</p>

 <h3>Prerequisites</h3>
    <ul>
        <li>.NET SDK 8 (or latest .NET version)</li>
        <li>Windows, macOS, or Linux operating system</li>
    </ul>
 <h3>Installation</h3>
    <ol>
        <li><strong>Clone the Repository:</strong></li>
        <pre><code>git clone https://github.com/username/document-sync-service.git</code></pre>

  <li><strong>Navigate to the Project Directory:</strong></li>
  <pre><code>cd document-sync-service</code></pre>

  <li><strong>Build the Project:</strong></li>
  <pre><code>dotnet build</code></pre>

  <li><strong>Publish the Project:</strong></li>
  <pre><code>dotnet publish -c Release -o ./publish</code></pre>

  <li><strong>Configure appsettings.json:</strong> Update the <code>appsettings.json</code> file with your source ,target and log directories:</li>
  <pre><code>
{
  "FileSettings": {
    "SourceDirectory": "C:\\Path\\To\\Source",
    "TargetDirectory": "C:\\Path\\To\\Target"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Path\\To\\Source\\log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
        </code></pre>
  </ol>

  <h3>Platform-Specific Configuration</h3>
    <h4>Windows</h4>
<ol>
  <li><strong>Install the Service:</strong></li>
  <pre><code>sc create DocumentSyncService binPath="C:\\Path\\To\\Published\\DocumentService.exe"</code></pre>

  <li><strong>Start the Service:</strong></li>
  <pre><code>sc start DocumentSyncService</code></pre>
</ol>

  <h4>macOS and Linux</h4>
  <ol>
    <li><strong>Install the .NET SDK:</strong></li>
    <ul>
    <li><strong>macOS:</strong></li>
    <pre><code>brew install --cask dotnet-sdk</code></pre>

  <li><strong>Linux (Ubuntu):</strong></li>
  <pre><code>sudo apt-get install dotnet-sdk</code></pre>
</ul>

  <li><strong>Publish and Run the Service:</strong> Move the published files to a directory of your choice</li>

  <li><strong>Create a systemd service file:</strong> Create a file at <code>/etc/systemd/system/document-sync-service.service</code></li>
    <pre><code>
[Unit]
Description=Document Sync Service

[Service]
ExecStart=/usr/local/bin/document-sync-service/DocumentService
Restart=always
User=nobody
Group=nogroup

[Install]
WantedBy=multi-user.target
        </code></pre>

  <li><strong>Enable and start the service:</strong></li>
  <pre><code>sudo systemctl enable document-sync-service
sudo systemctl start document-sync-service</code></pre>
    </ol>
 <h2>Usage</h2>
    <p>The Document Sync Service monitors the specified SourceDirectory for file changes (create, update, rename, delete) and synchronizes these changes to TargetDirectory. It logs all operations to the specified log file.</p>

  <h2>Contributing</h2>
  <p>Contributions are welcome! Please fork the repository and submit pull requests for any improvements, bug fixes, or features.</p>

  <h2>License</h2>
  <p>This project is licensed under the MIT License - see the LICENSE file for details.</p>
