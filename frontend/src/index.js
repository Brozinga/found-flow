import React from 'react';
import ReactDOM from 'react-dom';
import 'assets/css/App.css';
import { BrowserRouter , Route, Switch, Redirect } from 'react-router-dom';
import AdminLayout from 'layouts/admin';
import Auth from 'layouts/auth';
import { ChakraProvider } from '@chakra-ui/react';
import theme from 'theme/theme';
import { ThemeEditorProvider } from '@hypertheme-editor/chakra-ui';

ReactDOM.render(
	<ChakraProvider theme={theme}>
		<React.StrictMode>
			<ThemeEditorProvider>
				<BrowserRouter >
					<Switch>
						<Route path={`/auth`} component={Auth} />
						<Route path={`/app`} component={AdminLayout} />
						<Redirect from='/' to='/app' />
					</Switch>
				</BrowserRouter >
			</ThemeEditorProvider>
		</React.StrictMode>
	</ChakraProvider>,
	document.getElementById('root')
);
