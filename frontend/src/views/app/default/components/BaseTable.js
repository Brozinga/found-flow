import DataTable from 'react-data-table-component';
import {
    Flex,
    Text,
    useColorModeValue, Box, Icon,
} from "@chakra-ui/react";
import React from "react";
import { Badge } from "@chakra-ui/react"

// Custom components
import Card from "components/card/Card";
import Menu from "components/menu/MainMenu";
import { MdCallMade, MdCallReceived } from "react-icons/md";



const columns = [
    {
        name: 'Titulo',
        selector: row => row.title,
        sortable: true,
    },
    {
        name: 'Tipo',
        selector: row => { return row.type === "receita" ?
            <Icon w='22px' h='22px' color='green.500' as={MdCallMade} /> :
            <Icon w='22px' h='22px' color='red.500' as={MdCallReceived} /> },
    },
    {
        name: 'Categoria',
        selector: row => { return <Badge colorScheme="blue">{row.category}</Badge> },
        sortable: true,
    },
    {
        name: 'Valor',
        selector: row => row.value,
        sortable: true,
    },
    {
        name: 'Data Cadastro',
        selector: row => row.date,
        sortable: true,
    },
];

const data = [
    {
        id: 1,
        title: 'Salário',
        type: 'receita',
        category: 'trabalho',
        value: '10000.00',
        date: '12/02/2024'
    },
    {
        id: 2,
        title: 'Freelancer',
        type: 'receita',
        category: 'trabalho',
        value: '3000.00',
        date: '12/02/2024'
    },
    {
        id: 3,
        title: 'Almoço',
        type: 'despesa',
        category: 'trabalho',
        value: '120.00',
        date: '12/02/2024'
    },
    {
        id: 4,
        title: 'Salário',
        type: 'receita',
        category: 'trabalho',
        value: '10000.00',
        date: '12/02/2024'
    },
    {
        id: 5,
        title: 'Freelancer',
        type: 'receita',
        category: 'trabalho',
        value: '3000.00',
        date: '12/02/2024'
    },
    {
        id: 6,
        title: 'Almoço',
        type: 'despesa',
        category: 'trabalho',
        value: '120.00',
        date: '12/02/2024'
    },
    {
        id: 7,
        title: 'Salário',
        type: 'receita',
        category: 'trabalho',
        value: '10000.00',
        date: '12/02/2024'
    },
    {
        id: 8,
        title: 'Freelancer',
        type: 'receita',
        category: 'trabalho',
        value: '3000.00',
        date: '12/02/2024'
    },
    {
        id: 9,
        title: 'Almoço',
        type: 'despesa',
        category: 'trabalho',
        value: '120.00',
        date: '12/02/2024'
    },
    {
        id: 10,
        title: 'Salário',
        type: 'receita',
        category: 'trabalho',
        value: '10000.00',
        date: '12/02/2024'
    },
    {
        id: 11,
        title: 'Freelancer',
        type: 'receita',
        category: 'trabalho',
        value: '3000.00',
        date: '12/02/2024'
    },
    {
        id: 12,
        title: 'Almoço',
        type: 'despesa',
        category: 'trabalho',
        value: '120.00',
        date: '12/02/2024'
    }
]

const customStyles = {
    rows: {
        style: {
            minHeight: '46px',
            textAlign: 'start',
            paddingTop: 'var(--chakra-space-4)',
            paddingBottom: 'var(--chakra-space-4)',
            lineHeight: 'var(--chakra-lineHeights-5)',
            '&:not(:last-of-type)': {
                borderBottomStyle: 'solid',
                borderBottomWidth: '1px',
                borderBottomColor: 'var(--chakra-colors-gray-200)',
            },
        },
    },
    headCells: {
        style: {
            fontSize: 'var(--chakra-fontSizes-md)',
            textAlign: 'start',
            color: 'var(--chakra-colors-secondaryGray-600)',
        },
    },
    headRow: {
        style: {
            minHeight: '52px',
            borderBottomWidth: '1px',
            borderBottomColor: 'var(--chakra-colors-gray-200)',
            borderBottomStyle: 'solid',
        },
        denseStyle: {
            minHeight: '32px',
        },
    },
    cells: {
        style: {
            fontSize: 'var(--chakra-fontSizes-md)',
            fontWeight: '700',
            borderColor: 'var(--chakra-colors-gray-200)',
            color: 'var(--chakra-colors-secondaryGray-900)'
        },
    },
    pagination: {
        style: {
            color: 'var(--chakra-colors-secondaryGray-600)',
            fontSize: 'var(--chakra-fontSizes-md)',
            minHeight: '56px',
            borderTopStyle: 'solid',
            borderTopWidth: '1px',
            borderTopColor: 'var(--chakra-colors-gray-200)',
        },
        pageButtonsStyle: {
            borderRadius: '50%',
            height: '48px',
            width: '48px',
            padding: '10px',
            cursor: 'pointer',
            transition: '0.4s',
            fill: 'var(--chakra-colors-brand-300)',
            backgroundColor: 'transparent',
            '&>svg': {
                width: '28px',
                height: '28px',
            },
            '&:disabled': {
                cursor: 'unset',
                color: 'var(--chakra-colors-secondaryGray-300)',
                fill: 'var(--chakra-colors-secondaryGray-100)',
            },
            '&:hover:not(:disabled)': {
                backgroundColor: 'var(--chakra-colors-secondaryGray-400)',
            },
            '&:focus': {
                outline: 'none',
                backgroundColor: 'var(--chakra-colors-brand-500)',
            },
        },
    }
};

function BaseTable(props) {
    const { ...rest } = props;
    const textColor = useColorModeValue("secondaryGray.900", "white");

    return (
        <Card
            direction='column'
            w='100%'
            px='0px'
            overflowX={{ sm: "scroll", lg: "hidden" }}
            {...rest}>
            <Flex px='25px' justify='space-between' mb='10px' align='center'>
                <Text
                    color={textColor}
                    fontSize='22px'
                    fontWeight='700'
                    lineHeight='100%'>
                    Complex Table
                </Text>
                <Menu />
            </Flex>
            <Box p='20px'>
                <DataTable
                    columns={columns}
                    data={data}
                    customStyles={customStyles}
                    pagination
                />
            </Box>
        </Card>
    );
};

export default BaseTable