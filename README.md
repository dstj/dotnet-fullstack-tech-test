## Technical Stack

* Angular 15
* .NET 8

### Database

**Note: not needed to start the test**

* Create a folder for DB Data: `mkdir -p c:\docker\pgdev`
* Get the Docker image: `docker pull postgres:14`
* Run the container, command below:
    ```bash
    docker run --name pgdev -e POSTGRES_PASSWORD=Str0ngP@ssword -d -p 5432:5432 -v C:\Docker\pgdev:/var/lib/postgresql/data postgres:14
    ```

## Troubleshooting

Some environments get the .NET error "There was an error exporting the HTTPS certificate to a file..." when trying to do `dotnet run`. To resolve, perform the following:
  * Run `dotnet dev-certs https -v` to validate that you have the Developper Certificate setup
  * Ensure the empty folder `%APPDATA%\ASP.NET\https` is exists.