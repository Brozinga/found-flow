import React from "react";
import {NavLink} from "react-router-dom";
import {Toaster} from 'react-hot-toast';
import {
    validateName,
    validateEmail,
    validatePassword,
    validatePasswordConfirm
} from "../../../shared/Validations";
// Chakra imports
import {
    Box,
    Button,
    Flex,
    FormControl,
    FormLabel,
    Heading,
    Icon,
    Input,
    InputGroup,
    InputRightElement,
    Text,
    useColorModeValue,
} from "@chakra-ui/react";
// Custom components
import DefaultAuth from "layouts/auth/Default";
// Assets
import illustration from "assets/img/auth/auth.png";
import {MdOutlineRemoveRedEye} from "react-icons/md";
import {RiEyeCloseLine} from "react-icons/ri";
import SwitchField from "../../../components/fields/SwitchField";
import {notifyError} from "../../../components/notifications/Types";

function Register() {
    const textColor = useColorModeValue("navy.700", "white");
    const textColorSecondary = "gray.400";
    const textColorDetails = useColorModeValue("navy.700", "secondaryGray.600");
    const textColorBrand = useColorModeValue("brand.500", "white");
    const brandStars = useColorModeValue("brand.500", "brand.400");

    const [show, setShow] = React.useState(false);
    const [showConfirm, setShowConfirm] = React.useState(false);
    const showPassword = () => setShow(!show);
    const showPasswordConfirm = () => setShowConfirm(!showConfirm);

    const [userName, setUserName] = React.useState("");
    const [email, setEmail] = React.useState("");
    const [password, setPassword] = React.useState("");
    const [passwordConfirm, setPasswordConfirm] = React.useState("");
    const [notification, setNotification] = React.useState(true);

    const handleSubmit = () => {
        console.log("UserName: " + userName);
        console.log("Email: " + email);
        console.log("Password: " + password);
        console.log("PasswordConfirm: " + passwordConfirm);
        console.log("Notification: " + notification);

        let errors = "";
        errors += validateName(userName);
        errors += validateEmail(email);
        errors += validatePassword(password);
        errors += validatePasswordConfirm(password, passwordConfirm);
        notifyError(errors)
    }

    return (
        <DefaultAuth illustrationBackground={illustration} image={illustration}>
            <Flex maxW={{base: "100%", md: "max-content"}}
                  w='100%'
                  mx={{base: "auto", lg: "0px"}}
                  me='auto'
                  h='100%'
                  alignItems='center'
                  justifyContent='center'
                  mb={{base: "30px", md: "60px"}}
                  px={{base: "25px", md: "0px"}}
                  mt={{base: "40px", md: "14vh"}}
                  flexDirection='column'>
                <Flex flexDirection='column'>
                    <Box me='auto'>
                        <Heading color={textColor} fontSize='36px' mb='10px'>
                            Bora se cadastrar?
                        </Heading>
                        <Text
                            mb='36px'
                            ms='4px'
                            color={textColorSecondary}
                            fontWeight='400'
                            fontSize='md'>
                            Coloque os seus dados e vamos começar.
                        </Text>
                    </Box>
                    <Flex
                        zIndex='2'
                        direction='column'
                        w={{base: "100%", md: "420px"}}
                        maxW='100%'
                        background='transparent'
                        borderRadius='15px'
                        mx={{base: "auto", lg: "unset"}}
                        me='auto'
                        mb={{base: "20px", md: "auto"}}>
                        <FormControl>
                            <FormLabel
                                display='flex'
                                ms='4px'
                                fontSize='sm'
                                fontWeight='500'
                                color={textColor}
                                mb='8px'>
                                Nome<Text color={brandStars}>*</Text>
                            </FormLabel>
                            <Input
                                isInvalid={true}
                                isRequired={true}
                                variant='auth'
                                fontSize='sm'
                                ms={{base: "0px", md: "0px"}}
                                type='text'
                                placeholder='Nome completo'
                                mb='24px'
                                fontWeight='500'
                                size='lg'
                                value={userName}
                                onChange={(e) => setUserName(e.target.value)}
                            />
                            <FormLabel
                                display='flex'
                                ms='4px'
                                fontSize='sm'
                                fontWeight='500'
                                color={textColor}
                                mb='8px'>
                                Email<Text color={brandStars}>*</Text>
                            </FormLabel>
                            <Input
                                isRequired={true}
                                variant='auth'
                                fontSize='sm'
                                ms={{base: "0px", md: "0px"}}
                                type='email'
                                placeholder='meu@email.com'
                                mb='24px'
                                fontWeight='500'
                                size='lg'
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                            />
                            <FormLabel
                                ms='4px'
                                fontSize='sm'
                                fontWeight='500'
                                color={textColor}
                                display='flex'>
                                Senha<Text color={brandStars}>*</Text>
                            </FormLabel>
                            <InputGroup size='md'>
                                <Input
                                    isRequired={true}
                                    fontSize='sm'
                                    placeholder='Min. 6 caracteres'
                                    mb='24px'
                                    size='lg'
                                    type={show ? "text" : "password"}
                                    variant='auth'
                                    value={password}
                                    onChange={e => setPassword(e.target.value)}
                                />
                                <InputRightElement display='flex' alignItems='center' mt='4px'>
                                    <Icon
                                        color={textColorSecondary}
                                        _hover={{cursor: "pointer"}}
                                        as={show ? RiEyeCloseLine : MdOutlineRemoveRedEye}
                                        onClick={showPassword}
                                    />
                                </InputRightElement>
                            </InputGroup>
                            <FormLabel
                                ms='4px'
                                fontSize='sm'
                                fontWeight='500'
                                color={textColor}
                                display='flex'>
                                Confirmação de Senha<Text color={brandStars}>*</Text>
                            </FormLabel>
                            <InputGroup size='md'>
                                <Input
                                    isRequired={true}
                                    fontSize='sm'
                                    placeholder='Min. 6 caracteres'
                                    mb='24px'
                                    size='lg'
                                    type={showConfirm ? "text" : "password"}
                                    variant='auth'
                                    value={passwordConfirm}
                                    onChange={e => setPasswordConfirm(e.target.value)}
                                />
                                <InputRightElement display='flex' alignItems='center' mt='4px'>
                                    <Icon
                                        color={textColorSecondary}
                                        _hover={{cursor: "pointer"}}
                                        as={showConfirm ? RiEyeCloseLine : MdOutlineRemoveRedEye}
                                        onClick={showPasswordConfirm}
                                    />
                                </InputRightElement>
                            </InputGroup>
                            <SwitchField
                                isChecked={notification}
                                reversed={true}
                                fontSize="sm"
                                mb="20px"
                                id="notification"
                                label="Receber notificações"
                                onChange={() => setNotification(!notification)}
                            />
                            <Button
                                fontSize='sm'
                                variant='brand'
                                fontWeight='500'
                                w='100%'
                                h='50'
                                onClick={handleSubmit}
                                mb='24px'>
                                Concluir
                            </Button>
                        </FormControl>
                        <Flex
                            flexDirection='column'
                            justifyContent='center'
                            alignItems='start'
                            maxW='100%'
                            mt='0px'>
                            <Text color={textColorDetails} fontWeight='400' fontSize='14px'>
                                Ja está registrado?
                                <NavLink to='/auth/sign-in'>
                                    <Text
                                        color={textColorBrand}
                                        as='span'
                                        ms='5px'
                                        fontWeight='500'>
                                        Entre!
                                    </Text>
                                </NavLink>
                            </Text>
                        </Flex>
                    </Flex>
                </Flex>
            </Flex>
            <Toaster/>
        </DefaultAuth>
    );
}

export default Register;
