#!/bin/bash

printf "Pick an environment\n1.DEV\n2.UAT\n3.PRODUCTION\n"
read val

re='^[1-3]+$'

if ! [[ $val =~ $re ]] ; then
   echo "error: Not a number OR greater than 3 OR less than 1" >&2; exit 1
fi

if [[ $val -eq 1 ]] ; then
   tst=DEV
elif [[ $val -eq 2 ]] ; then
   tst=UAT
elif [[ $val -eq 3 ]] ; then
   tst=PROD
fi

echo Your had choosen environment $tst
docker-compose -f docker-compose.yml -f docker-compose.$tst.yml up -d