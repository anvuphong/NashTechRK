import React from "react";
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import BookDetailPage from "./Pages/BookDetailPage/BookDetailPage";
import BookListPage from "./Pages/BookListPage/BookListPage";
import BookRequestPage from "./Pages/BookRequestPage/BookRequestPage";
import HomePage from "./Pages/HomePage/HomePage";
import LibraryManagementPage from "./Pages/LibraryManagementPage/LibraryManagementPage";


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
    // {
    //     path: PATH.LOGIN,
    //     element: (<LoginPage />)
    // }
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
    }
]

const App = () => {
    return (
        <div>
            <BrowserRouter>
                <ul>
                    {navbarItem.map(item => (
                        <li key={item.to} ><Link className="link" to={item.to}>{item.title}</Link></li>
                    ))}
                </ul>
                <Routes>
                    {routes.map(route => (
                        <Route key={route.path} path={route.path} element={route.element} />
                    ))}
                    <Route path={`book/:bookId`} element={<BookDetailPage/>}/>
                    {/* <Route path={`management/:managementId`} element={<BookDetailPage/>}/> */}
                </Routes>
            </BrowserRouter>
        </div>
    )
}
export default App;
