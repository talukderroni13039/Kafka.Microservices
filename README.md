# Kafka.Microservices

First of all you need to install Docker desktop app.
Then save the docker-compose.yml after that go to the directory where this file is saved.

        cd /home/s3user/deployment
        docker compose -f docker-compose.yml stop
        docker compose -f docker-compose.yml up -d

you can also restart via docker desktop app.
Make sure all services are running using following command:
docker ps.

To redeploy any image :

                docker compose -f docker-compose-sftp-splitter-beta.yaml stop
                docker ps -a
                docker rm container_id
                docker images
                docker rmi 41f3c604f0de
                docker compose -f docker-compose-sftp-splitter-beta.yaml up -d 
                docker ps

Here is producer and consumer API as well as follow consumer1 and consumer2 which is console app.
