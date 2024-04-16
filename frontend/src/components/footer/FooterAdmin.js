/*eslint-disable*/
import React from "react";
import {
    Flex,
    Link,
    List,
    ListItem,
    Text,
    Button,
    useColorMode,
    useColorModeValue,
} from "@chakra-ui/react";

export default function Footer() {
    const textColor = useColorModeValue("gray.400", "white");
    return (
        <Flex
            zIndex='3'
            flexDirection={{
                base: "row",
                xl: "row",
            }}
            alignItems={{
                base: "center",
                xl: "center",
            }}
            justifyContent='center'
            px={{base: "30px", md: "50px"}}
            pb='30px'>
            <Text
                color={textColor}
                textAlign={{
                    base: "center",
                    xl: "start",
                }}
                mb={{base: "20px", xl: "0px"}}>
                {" "}
                &copy; {1900 + new Date().getYear()}
                <Text as='span' fontWeight='500' ms='4px'>
                    Todos os direitos reservados para
                    <Link
                        mx='3px'
                        color={textColor}
                        href='https://github.com/brozinga'
                        target='_blank'
                        fontWeight='700'>
                        BrozTech
                    </Link>
                    e interface por
                    <Link
                        mx='3px'
                        color={textColor}
                        href='https://www.simmmple.com?ref=horizon-chakra-free'
                        target='_blank'
                        fontWeight='700'>
                        Simmmple!
                    </Link>
                </Text>
            </Text>
        </Flex>
    );
}
