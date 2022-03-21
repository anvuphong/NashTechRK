import React from "react";
import { Button, Container, Nav, Navbar } from "react-bootstrap";
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import BookDetailPage from "./Pages/BookDetailPage/BookDetailPage";
import BookListPage from "./Pages/BookListPage/BookListPage";
import BookRequestPage from "./Pages/BookRequestPage/BookRequestPage";
import HomePage from "./Pages/HomePage/HomePage";
import LibraryManagementPage from "./Pages/LibraryManagementPage/LibraryManagementPage";
import LoginPage from "./Pages/LoginPage/LoginPage";


const PATH = {
    HOME: '/',
    BOOKS: '/book',
    REQUEST: '/request',
    MANAGEMENT: '/management',
    LOGIN: '/login',
};

const routes = [
    {
        path: PATH.HOME,
        element: (<HomePage />)
    },
    {
        path: PATH.BOOKS,
        element: (<BookListPage />)
    },
    {
        path: PATH.REQUEST,
        element: (<BookRequestPage />)
    },
    {
        path: PATH.MANAGEMENT,
        element: (<LibraryManagementPage />)
    },
    {
        path: PATH.LOGIN,
        element: (<LoginPage />)
    }
]

const navbarItem = [
    {
        to: PATH.HOME,
        title: 'Home'
    },
    {
        to: PATH.BOOKS,
        title: 'Books'
    },
    {
        to: PATH.REQUEST,
        title: 'Request List'
    },
    {
        to: PATH.MANAGEMENT,
        title: 'Library Management'
    },
    // {
    //     to: PATH.LOGIN,
    //     title: 'Login'
    // }
]

const App = () => {
    const token = localStorage.getItem('token');

    function onLogoutClicked() {
        localStorage.setItem('token', '');
        localStorage.setItem('userId', '');
        window.location.reload();
    }

    return (
        <div>
            <BrowserRouter>
                <Navbar bg="light" variant="light" expand="lg" style={{ marginBottom: '50px' }}>
                    <Container>
                        <Navbar.Brand as={Link} to={PATH.HOME}>Library</Navbar.Brand>
                        <Nav className="me-auto">
                            {navbarItem.map(item => (
                                <Nav.Link key={item.to} as={Link} to={item.to}>
                                    {item.title}
                                </Nav.Link>
                            ))}
                            {!token ? (
                                <Nav.Link as={Link} style={{ float: "right" }} to="/login">Login</Nav.Link>
                            ) : (
                                <Nav.Link onClick={onLogoutClicked}>Logout</Nav.Link>
                            )}
                        </Nav>
                    </Container>
                </Navbar>
                <Routes>
                    {routes.map(route => (
                        <Route key={route.path} path={route.path} element={route.element} />
                    ))}
                    <Route path={`book/:bookId`} element={<BookDetailPage />} />
                    <Route
                        path="*"
                        element={
                            <main style={{ padding: "1rem" }}>
                                <p>There's nothing here!</p>
                            </main>
                        }
                    />
                </Routes>
            </BrowserRouter>
        </div>
    )
}
export default App;
