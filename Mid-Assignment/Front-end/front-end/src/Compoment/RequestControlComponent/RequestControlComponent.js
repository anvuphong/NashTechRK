import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";

const RequestControlComponet = () => {
    const [requestList, setRequestList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');

    console.log(requestList)
    useEffect(() => {
        (async () => {
            console.log('hi')
            let isCancel = false;
            //get category
            await axios({
                method: 'GET',
                url: 'https://localhost:7279/api/book-request'
            }).then(response => {
                if (!isCancel) {
                    setRequestList(response.data);
                    setIsLoading(false);
                }
            }).catch((error) => {
                if (!isCancel) {
                    setIsLoading(false);
                    setError('Something wrong');
                }
            });

            // //get books
            // await axios({
            //     method: 'GET',
            //     url: 'https://localhost:7279/api/book'
            // }).then(response => {
            //     if (!isCancel) {
            //         setBooks(response.data);
            //         setBooksFilter(response.data);
            //         setIsLoading(false);
            //     }
            // }).catch((error) => {
            //     if (!isCancel) {
            //         setIsLoading(false);
            //         setError('Something wrong');
            //     }
            // });
            return () => {
                isCancel = true;
            }
        })()
    }, []);

    //Status Request
    if (isLoading) return (
        <div>Loading</div>
    );
    if (error) return (
        <div style={{ color: 'red' }}>{error}</div>
    );

    return (
        <Table striped bordered hover className="categories-table">
            <thead>
                <tr>
                    <th>Category</th>
                    <th>Preview</th>
                </tr>
            </thead>
            {/* <tbody>
                {categoriesFilter.map(category => (
                    <tr key={category.categoryId}>
                        <td>{category.categoryName}</td>
                    </tr>
                ))}
            </tbody> */}
        </Table>
    )
}
export default RequestControlComponet;