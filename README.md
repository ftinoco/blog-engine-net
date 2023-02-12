# BlogEngineNet

## Getting started

- Cloning code
```
git clone https://github.com/ftinoco/blog-engine-net.git
```

- Build the whole solution in order to install all packages.
- Create database called "BlogEngineDB" in your local instance.
- Change string connection in "BlogEngineNet.API/appsettings.json" with your credentials.
- On Package Manager Console, select the project "BlogEngineNet.Persistence" and run the following command:
```
update-database -verbose
```