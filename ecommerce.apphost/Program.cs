var builder = DistributedApplication.CreateBuilder(args);

var prodApi = builder.AddProject<Projects.ecommerce_product>("apiservice-product");
var orderApi = builder.AddProject<Projects.ecommerce_order>("apiservice-order");
builder.AddProject<Projects.Ecommerce>("web-frontend")
    .WithReference(prodApi)
    .WithReference(orderApi);


builder.Build().Run();

