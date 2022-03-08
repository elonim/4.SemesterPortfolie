kubectl delete deployment platforms-depl --force
kubectl delete deployment commands-depl --force
kubectl delete deployment mssql-depl --force
kubectl delete deployment rabbitmq-depl --force

kubectl delete services commands-clusterip-srv --force
kubectl delete services mssql-clusterip-srv --force
kubectl delete services mssql-loadbalancer --force
kubectl delete services platforms-clusterip-srv --force
kubectl delete services platformservice-srv --force
kubectl delete services rabbitmq-clusterip-srv --force
kubectl delete services rabbitmq-loadbalancer --force

kubectl delete ingress ingress-srv --force