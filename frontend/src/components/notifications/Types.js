import toast from "react-hot-toast";
import {Alert, AlertDescription, AlertIcon, AlertTitle, Flex, ListItem, UnorderedList} from "@chakra-ui/react";
import React from "react";

export const notifyError = (errors) => {
    let errorArray = errors.split(";").filter(x => x !== "");
    console.log(errorArray);
    if (errorArray.length > 0) {
        toast(
            <Alert status="error" flexDirection="column">
                <Flex flexDirection="row">
                    <AlertIcon/>
                    <AlertTitle mr={2} mb={5}>Ops! Erros encontrados</AlertTitle>
                </Flex>
                <AlertDescription>
                    <UnorderedList>
                        {
                            errorArray.map((error, index) => (<ListItem key={index}>{error}</ListItem>))
                        }
                    </UnorderedList>
                </AlertDescription>
            </Alert>,
            {
                duration: 5000,
                position: 'bottom-right',
                style: {
                    margin: '0px',
                    background: '#FEEFEE',
                    maxWidth: "400px"
                },
            }
        )
    }
}

export const notifyWarning = (errors) => {
    let errorArray = errors.split(";").filter(x => x !== "");
    console.log(errorArray);
    if (errorArray.length > 0) {
        toast(
            <Alert status="warning" flexDirection="column">
                <Flex flexDirection="row">
                    <AlertIcon/>
                    <AlertTitle mr={2} mb={5}>Ops! Erros encontrados</AlertTitle>
                </Flex>
                <AlertDescription>
                    <UnorderedList>
                        {
                            errorArray.map((error, index) => (<ListItem key={index}>{error}</ListItem>))
                        }
                    </UnorderedList>
                </AlertDescription>
            </Alert>,
            {
                duration: 5000,
                position: 'bottom-right',
                style: {
                    margin: '0px',
                    background: '#FFF6DA',
                    maxWidth: "400px"
                },
            }
        )
    }
}