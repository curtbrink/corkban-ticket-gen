pipeline {
    agent any

    environment {
        BRANCH_NAME = "${env.BRANCH_NAME}";
        CORKBAN_ENV = "production";
        
        // todo: grab these from a different file
        REPO_NAME = "corkban-ticket-gen";
        VERSION = "0.1";
        
        DOCKER_IMAGE_NAME = "curtbrink/${env.REPO_NAME}:v${env.VERSION}";
        
        CORKBAN_DATA_DIR = credentials('corkban-data-dir');
    }

    stages {
        stage('Checkout') {
            steps {
                // Checkout the code from GitHub repository
                git branch: 'main', url: 'https://github.com/curtbrink/corkban-ticket-gen'
            }
        }
        
        stage('Dotnet Restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        
        stage('Dotnet Test') {
            steps {
                sh 'dotnet test'
            }
        }

        stage('Build Docker Image') {
            when { branch 'main' }
            steps {
                sh 'docker build -t $DOCKER_IMAGE_NAME .'
            }
        }

        stage('Remove Existing Docker Container') {
            when { branch 'main' }
            steps {
                script {
                    def containerExists = sh(script: "docker ps -a -q -f name=${env.REPO_NAME}", returnStdout: true).trim()
                    if (containerExists) {
                        // Stop and remove container if it's running
                        sh "docker stop ${env.REPO_NAME}"
                        sh "docker rm ${env.REPO_NAME}"
                    } else {
                        echo "Container ${env.REPO_NAME} does not exist, skipping stop/remove."
                    }
                }
            }
        }

        stage('Deploy Docker Container') {
            when { branch 'main' }
            steps {
                script {
                    // recreate container with new image
                    sh "docker run -d -p 34202:8080 --mount type=bind,src=${env.CORKBAN_DATA_DIR},dst=/db --name ${env.REPO_NAME} ${env.DOCKER_IMAGE_NAME}"
                }
            }
        }
    }
}