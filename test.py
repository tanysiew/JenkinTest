# Python 3
import subprocess

txt = input("Pick an environment\n1.DEV\n2.UAT\n3.PRODUCTION\n")
env = ""

if not txt.isnumeric():
    print("Only input number!")
    quit()

if int(txt) > 3 or int(txt) < 1:
    print("Only input number 1 to 3!")
    quit()

if int(txt) == 1:
    print("Choosen DEV")
    env = "DEV"
if int(txt) == 2:
    print("Choosen UAT")
    env = "UAT"
if int(txt) == 3:
    print("Choosen PRODUCTION")
    env = "PROD"

print("Starting docker-compose... ", env)

subprocess.call("docker-compose -f docker-compose.yml -f docker-compose.{env}.yml up", shell=True, stdout=output, stderr=output)

