echo "Switching to branch main............"
git checkout main

echo "Building backend............"
dotnet build --configuration Release

echo "Deploying backend............"
scp -i "C:/Users/thenu/Downloads/ssh/master-VM_key.pem" -r "D:\Integrify Assignments\fs15_Fullstack\backend\IntegrifyLibrary.Infrastructure\bin\Release\net7.0" zealinbee@98.71.53.99:/var/www/integrify-library-backend

echo "Deployed successfully!!!!!!!"