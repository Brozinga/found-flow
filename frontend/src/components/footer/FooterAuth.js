/*eslint-disable*/
import React from "react";
import {
  Flex,
  Link,
  List,
  ListItem,
  Text,
  useColorModeValue,
} from "@chakra-ui/react";

export default function Footer() {
  let textColor = useColorModeValue("gray.400", "white");
  let linkColor = useColorModeValue({ base: "gray.400", lg: "white" }, "white");
  return (
    <Flex
      zIndex='3'
      flexDirection={{
        base: "column",
        lg: "row",
      }}
      alignItems={{
        base: "center",
        xl: "start",
      }}
      justifyContent='space-between'
      px={{ base: "30px", md: "0px" }}
      pb='30px'>
      <Text
        color={textColor}
        textAlign={{
          base: "center",
          xl: "start",
        }}
        mb={{ base: "20px", lg: "0px" }}>
        {" "}
        &copy; {1900 + new Date().getYear()}
        <Text as='span' fontWeight='500' ms='4px'>
          Todos os direitos reservados a
            <Link
                mx='3px'
                color={textColor}
                href='https://github.com/brozinga'
                target='_blank'
                fontWeight='700'>
                Broztech
            </Link>
            e interface de
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
