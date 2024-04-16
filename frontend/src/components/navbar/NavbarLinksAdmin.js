// Chakra Imports
import {
	Avatar,
	Flex,
	Icon,
	Menu,
	MenuButton,
	MenuItem,
	MenuList,
	Text,
	useColorModeValue
} from '@chakra-ui/react';
// Custom Components
import { ItemContent } from 'components/menu/ItemContent';
import { SidebarResponsive } from 'components/sidebar/Sidebar';
import PropTypes from 'prop-types';
import React from 'react';
// Assets
import { MdNotificationsNone } from 'react-icons/md';
import routes from 'routes.js';
export default function HeaderLinks(props) {
	const { secondary } = props;
	// Chakra Color Mode
	const navbarIcon = useColorModeValue('gray.400', 'white');
	let menuBg = useColorModeValue('white', 'navy.800');
	const textColor = useColorModeValue('secondaryGray.900', 'white');
	const textColorBrand = useColorModeValue('brand.700', 'brand.400');
	const borderColor = useColorModeValue('#E6ECFA', 'rgba(135, 140, 189, 0.3)');
	const shadow = useColorModeValue(
		'14px 17px 40px 4px rgba(112, 144, 176, 0.18)',
		'14px 17px 40px 4px rgba(112, 144, 176, 0.06)'
	);
	return (
		<Flex
			w={{ sm: '100%', md: 'auto' }}
			alignItems="center"
			flexDirection="row"
			bg={menuBg}
			flexWrap={secondary ? { base: 'wrap', md: 'nowrap' } : 'unset'}
			p="10px"
			borderRadius="30px"
			boxShadow={shadow}>
			<SidebarResponsive routes={routes} />
			<Flex alignItems={"center"} w={"100%"} justifyContent={"end"}>
				<Menu>
					<MenuButton p="0px">
						<Icon mt="6px" as={MdNotificationsNone} color={navbarIcon} w="25px" h="25px" me="22px" ml="10px" />
					</MenuButton>
					<MenuList
						boxShadow={shadow}
						p="20px"
						borderRadius="20px"
						bg={menuBg}
						border="none"
						mt="22px"
						me={{ base: '30px', md: 'unset' }}
						minW={{ base: 'unset', md: '400px', xl: '450px' }}
						maxW={{ base: '360px', md: 'unset' }}>
						<Flex jusitfy="space-between" w="100%" mb="20px">
							<Text fontSize="md" fontWeight="600" color={textColor}>
								Notifications
							</Text>
							<Text fontSize="sm" fontWeight="500" color={textColorBrand} ms="auto" cursor="pointer">
								Mark all read
							</Text>
						</Flex>
						<Flex flexDirection="column">
							<MenuItem _hover={{ bg: 'none' }} _focus={{ bg: 'none' }} px="0" borderRadius="8px" mb="10px">
								<ItemContent info="Horizon UI Dashboard PRO" aName="Alicia" />
							</MenuItem>
							<MenuItem _hover={{ bg: 'none' }} _focus={{ bg: 'none' }} px="0" borderRadius="8px" mb="10px">
								<ItemContent info="Horizon Design System Free" aName="Josh Henry" />
							</MenuItem>
						</Flex>
					</MenuList>
				</Menu>
				<Menu>
					<MenuButton p="0px">
						<Avatar
							_hover={{ cursor: 'pointer' }}
							color="white"
							name="Adela Parkson"
							bg="#11047A"
							size="sm"
							w="40px"
							h="40px"
						/>
					</MenuButton>
					<MenuList boxShadow={shadow} p="0px" mt="10px" borderRadius="20px" bg={menuBg} border="none">
						<Flex w="100%" mb="0px">
							<Text
								ps="20px"
								pt="16px"
								pb="10px"
								w="100%"
								borderBottom="1px solid"
								borderColor={borderColor}
								fontSize="sm"
								fontWeight="700"
								color={textColor}>
								👋&nbsp; Oi, Adela
							</Text>
						</Flex>
						<Flex flexDirection="column" p="10px">
							<MenuItem _hover={{ bg: 'none' }} _focus={{ bg: 'none' }} borderRadius="8px" px="14px">
								<Text fontSize="sm">Perfil</Text>
							</MenuItem>
							<MenuItem
								_hover={{ bg: 'none' }}
								_focus={{ bg: 'none' }}
								color="red.400"
								borderRadius="8px"
								px="14px">
								<Text fontSize="sm">Deslogar</Text>
							</MenuItem>
						</Flex>
					</MenuList>
				</Menu>
			</Flex>
		</Flex>
	);
}

HeaderLinks.propTypes = {
	variant: PropTypes.string,
	fixed: PropTypes.bool,
	secondary: PropTypes.bool,
	onOpen: PropTypes.func
};
