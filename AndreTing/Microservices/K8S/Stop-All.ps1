$services = @(
    "commands-clusterip-srv",
    "mssql-clusterip-srv",
    "mssql-loadbalancer",
    "platforms-clusterip-srv",
    "platformservice-srv",
    "rabbitmq-clusterip-srv",
    "rabbitmq-loadbalancer"
)

$deployments = @(
    "platforms-depl",
    "rabbitmq-depl",
    "commands-depl",
    "mssql-depl"
)


foreach ($service in $services) {
    kubectl delete services $service
}

kubectl delete ingress ingress-srv

foreach ($depl in $deployments) {
    kubectl delete deployment  $depl
}