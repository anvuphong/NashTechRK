import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

const BookDetailPage = () => {
    const bookId = useParams().bookId;
    const [book, setBook] = useState({
        name: null,
        author: null
    });
    const [category, setCategory] = useState();
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');

    //get Book
    useEffect(() => {
        (async () => {
            let isCancel = false;
            let categoryId = 0;
            //getBookById api
            await axios({
                method: 'GET',
                url: `https://localhost:7279/api/book/${bookId}`
            }).then(respone => {
                if (!isCancel) {
                    setBook({
                        name: respone.data.bookName,
                        author: respone.data.author,
                    });
                    setIsLoading(false);
                    categoryId = respone.data.categoryId
                }
            }).catch((error) => {
                if (!isCancel) {
                    setIsLoading(false);
                    setError('Something wrong');
                }
            });

            //getCategoryById api
            await axios({
                method: 'GET',
                url: `https://localhost:7279/api/category/${categoryId}`
            }).then(respone => {
                if (!isCancel) {
                    setCategory(respone.data.categoryName);
                    setIsLoading(false);
                }
            }).catch((error) => {
                if (!isCancel) {
                    setIsLoading(false);
                    setError('Something wrong');
                }
            });
            return () => {
                isCancel = true;
            }
        })()
    }, [bookId]);

    //Request status
    if (isLoading) return (
        <div>Loading</div>
    )
    if (error) return (
        <div style={{ color: 'red' }}>{error}</div>
    )

    return (
        <div>
            <h1>BookDetailPage</h1>
            <p>Name: {book.name}</p>
            <p>Author: {book.author}</p>
            <p>Category: {category}</p>
        </div>
    )
}
export default BookDetailPage;