import {
  Box,
  Icon,
  SimpleGrid,
  useColorModeValue,
} from "@chakra-ui/react";
// Custom components
import MiniStatistics from "components/card/MiniStatistics";
import IconBox from "components/icons/IconBox";
import React from "react";
import {
  MdCallMade,
  MdAttachMoney,
  MdCallReceived,
} from "react-icons/md";
import BaseTable from "../default/components/BaseTable";
import TotalSpent from "../default/components/TotalSpent";

export default function UserReports() {
  // Chakra Color Mode
  const brandColor = useColorModeValue("brand.500", "white");
  const boxBg = useColorModeValue("secondaryGray.300", "whiteAlpha.100");
  return (
    <Box pt={{ base: "130px", md: "80px", xl: "80px" }}>
      <SimpleGrid
        columns={{ base: 1, md: 1, lg: 3, "2xl": 3 }}
        minH='150px'
        gap='20px'
        mb='20px'>
        <MiniStatistics
          startContent={
            <IconBox
              w='66px'
              h='66px'
              bg={boxBg}
              icon={
                <Icon w='42px' h='42px' alignItems={"center"} as={MdAttachMoney} color={brandColor} />
              }
            />
          }
          name='Saldo'
          value='R$ 999.999,99'
        />
          <MiniStatistics
              startContent={
                  <IconBox
                      w='66px'
                      h='66px'
                      bg={boxBg}
                      icon={
                          <Icon w='42px' h='42px' as={MdCallMade} color={brandColor} />
                      }
                  />
              }
              name='Receitas'
              value='R$ 999.999,99'
          />
          <MiniStatistics
              startContent={
                  <IconBox
                      w='66px'
                      h='66px'
                      bg={boxBg}
                      icon={
                          <Icon w='42px' h='42px' as={MdCallReceived} color={brandColor} />
                      }
                  />
              }
              name='Despesas'
              value='R$ 999.999,99'
          />
      </SimpleGrid>

      <SimpleGrid columns={{ base: 1, md: 1, xl: 1 }} gap='20px' mb='20px'>
        <TotalSpent />
          <BaseTable />
      </SimpleGrid>
    </Box>
  );
}
