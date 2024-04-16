// Chakra imports
import {
  Box,
  Flex,
  Icon,
  Text,
} from "@chakra-ui/react";
// Custom components
import Card from "components/card/Card.js";
import LineChart from "components/charts/LineChart";
import React from "react";
// Assets
import { RiArrowUpSFill } from "react-icons/ri";
import {
  lineChartDataTotalSpent,
  lineChartOptionsTotalSpent,
} from "variables/charts";

export default function TotalSpent(props) {
  const { ...rest } = props;

  // Chakra Color Mode
  return (
    <Card
      justifyContent='center'
      align='center'
      direction='column'
      w='100%'
      mb='0px'
      {...rest}>
      <Flex me='20px'>
        <Flex align='center' mb='20px'>
          <Text
              color='secondaryGray.600'
              fontSize='md'
              fontWeight='500'
              mt='4px'
              me='12px'>
            Resultados
          </Text>
          <Flex align='center'>
            <Icon as={RiArrowUpSFill} color='green.500' me='2px' mt='2px' />
            <Text color='green.500' fontSize='2xl' fontWeight='700'>
              +2.45%
            </Text>
          </Flex>
        </Flex>
      </Flex>
      <Flex w='100%' flexDirection={{ base: "column", lg: "row" }}>
        <Box minH='260px' w='100%' minW='75%' mt='auto'>
          <LineChart
            chartData={lineChartDataTotalSpent}
            chartOptions={lineChartOptionsTotalSpent}
          />
        </Box>
      </Flex>
    </Card>
  );
}
